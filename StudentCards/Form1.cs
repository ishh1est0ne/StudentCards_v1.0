using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

namespace StudentCards
{
    public partial class Form1 : Form
    {
        public class Student
        {
            public string Iin { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string DateOfBirth { get; set; }
            public string Group { get; set; }
            public string Speciality { get; set; }
            public Image Photo { get; set; }
        }

        private List<Student> studentsToPrint = new List<Student>();
        private Image templateImage;
        private int currentPrintIndex = 0;
        private int currentCardNumber = 1;
        private string logFilePath = "print_log.txt";

        public Form1()
        {
            InitializeComponent();

            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("StudentCardsApp");
            }
            catch
            {
#pragma warning disable CS0618
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
#pragma warning restore CS0618
            }
        }

        private void Log(string message)
        {
            string line = $"[{DateTime.Now:HH:mm:ss}] {message}";
            try
            {
                File.AppendAllText(logFilePath, line + Environment.NewLine);
            }
            catch { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Log("--- СТАРТ НОВОЙ ЗАДАЧИ ПЕЧАТИ ---");
            try
            {
                if (!File.Exists(txtExcel.Text))
                {
                    MessageBox.Show("Файл Excel не найден по указанному пути!");
                    return;
                }

                if (!File.Exists(txtTemplate.Text))
                {
                    MessageBox.Show("Файл шаблона не найден по указанному пути!");
                    return;
                }

                LoadExcelData();

                templateImage = Image.FromFile(txtTemplate.Text);
                Log($"Шаблон загружен успешно. Разрешение: {templateImage.Width}x{templateImage.Height}");

                currentCardNumber = (int)numCardStart.Value;
                currentPrintIndex = 0;

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Landscape = true;

                PrintDialog pdi = new PrintDialog();
                pdi.Document = pd;

                if (pdi.ShowDialog() == DialogResult.OK)
                {
                    Log($"Выбран принтер: {pd.PrinterSettings.PrinterName}. Начинается отправка...");
                    pd.PrintPage += Pd_PrintPage;
                    pd.Print();
                    Log("Все страницы успешно отправлены в очередь печати.");
                    MessageBox.Show("Печать завершена успешно!");
                }
                else
                {
                    Log("Печать отменена пользователем в диалоговом окне.");
                }
            }
            catch (Exception ex)
            {
                Log($"КРИТИЧЕСКАЯ ОШИБКА: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Ошибка: {ex.Message}\nПодробности записаны в {logFilePath}");
            }
            finally
            {
                if (templateImage != null)
                {
                    templateImage.Dispose();
                    templateImage = null;
                }

                foreach (var s in studentsToPrint)
                {
                    if (s.Photo != null)
                    {
                        s.Photo.Dispose();
                        s.Photo = null;
                    }
                }
            }
        }

        private void LoadExcelData()
        {
            studentsToPrint.Clear();
            Log($"Открытие файла: {txtExcel.Text}");

            using (var package = new ExcelPackage(new FileInfo(txtExcel.Text)))
            {
                if (package.Workbook.Worksheets.Count == 0)
                    throw new Exception("В файле Excel нет листов!");

                var ws = package.Workbook.Worksheets[0];
                int totalRows = ws.Dimension?.Rows ?? 0;
                Log($"Лист '{ws.Name}' загружен. Всего строк: {totalRows}");

                var pictures = ws.Drawings.OfType<ExcelPicture>().ToList();
                Log($"Найдено графических объектов (фото) в таблице: {pictures.Count}");

                int startRow = (int)numStart.Value + 1; // 1-я строка заголовки
                int endRow = (int)numEnd.Value + 1;
                if (endRow > totalRows) endRow = totalRows;

                Log($"Диапазон чтения строк Excel: с {startRow} по {endRow}");

                for (int row = startRow; row <= endRow; row++)
                {
                    string iin = ws.Cells[row, 1].Text.Trim();
                    if (string.IsNullOrEmpty(iin))
                    {
                        Log($"Строка {row}: ИИН пустой, пропуск строки.");
                        continue;
                    }

                    Image studentPhoto = null;

                    var pic = pictures.FirstOrDefault(p => p.From.Row == row - 1);
                    if (pic != null)
                    {
                        try
                        {
                            using (var ms = new MemoryStream(pic.Image.ImageBytes))
                            {
                                studentPhoto = Image.FromStream(ms);
                            }
                            Log($"Строка {row} (ИИН: {iin}): фото успешно извлечено.");
                        }
                        catch (Exception pEx)
                        {
                            Log($"Строка {row} (ИИН: {iin}): сбой чтения картинки -> {pEx.Message}");
                        }
                    }
                    else
                    {
                        Log($"ВНИМАНИЕ: Строка {row} (ИИН: {iin}): фото не найдено в ячейке!");
                    }

                    studentsToPrint.Add(new Student
                    {
                        Iin = iin,
                        LastName = ws.Cells[row, 2].Text.Trim(),
                        FirstName = ws.Cells[row, 3].Text.Trim(),
                        DateOfBirth = ws.Cells[row, 4].Text.Trim(),
                        Group = ws.Cells[row, 5].Text.Trim(),
                        Speciality = ws.Cells[row, 6].Text.Trim(),
                        Photo = studentPhoto
                    });
                }
            }

            Log($"Готово к печати студентов: {studentsToPrint.Count}");
            if (studentsToPrint.Count == 0)
                throw new Exception("Не удалось найти ни одного студента в выбранном диапазоне.");
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (currentPrintIndex >= studentsToPrint.Count)
            {
                e.HasMorePages = false;
                return;
            }

            Student student = studentsToPrint[currentPrintIndex];
            Log($"Печать карточки [{currentPrintIndex + 1}/{studentsToPrint.Count}]: {student.LastName} {student.FirstName} (Карта №{currentCardNumber})");

            using (Bitmap cardImg = GenerateCard(student, currentCardNumber, templateImage))
            {
                e.Graphics.DrawImage(cardImg, e.PageBounds);
            }

            currentPrintIndex++;
            currentCardNumber++;

            e.HasMorePages = currentPrintIndex < studentsToPrint.Count;
        }

        private Bitmap GenerateCard(Student student, int cardNumber, Image template)
        {
            Bitmap bmp = new Bitmap(template.Width, template.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                g.DrawImage(template, 0, 0, template.Width, template.Height);

                if (student.Photo != null)
                {
                    Rectangle photoRect = new Rectangle(36, 96, 299, 391);
                    g.DrawImage(student.Photo, photoRect);
                }

                using (Font fontBase = new Font("Segoe UI Black", 24, GraphicsUnit.Pixel))
                using (Font fontNum = new Font("Segoe UI Black", 28, GraphicsUnit.Pixel))
                {
                    g.DrawString(student.LastName, fontBase, Brushes.Black, new Point(359, 157));
                    g.DrawString(student.FirstName, fontBase, Brushes.Black, new Point(359, 233));
                    g.DrawString(student.DateOfBirth, fontBase, Brushes.Black, new Point(359, 306));
                    g.DrawString(student.Group, fontBase, Brushes.Black, new Point(359, 383));
                    g.DrawString(student.Speciality, fontBase, Brushes.Black, new Point(359, 462));

                    g.DrawString(student.Iin, fontBase, Brushes.Black, new Point(111, 530));
                    g.DrawString(cardNumber.ToString(), fontNum, Brushes.Black, new Point(888, 127));
                }
            }
            return bmp;
        }

        
        private void Form1_Load(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            btnPrint_Click(sender, e);
        }

        private void numStart_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numEnd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numCardStart_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtExcel_TextChanged(object sender, EventArgs e)
        {

        }
    }
}