namespace StudentCards
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtExcel = new System.Windows.Forms.TextBox();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numEnd = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numCardStart = new System.Windows.Forms.NumericUpDown();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCardStart)).BeginInit();
            this.SuspendLayout();
            // 
            // txtExcel
            // 
            this.txtExcel.Location = new System.Drawing.Point(18, 18);
            this.txtExcel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtExcel.Name = "txtExcel";
            this.txtExcel.Size = new System.Drawing.Size(246, 31);
            this.txtExcel.TabIndex = 0;
            this.txtExcel.Text = "Сюда Путь К Excel";
            this.txtExcel.TextChanged += new System.EventHandler(this.txtExcel_TextChanged);
            // 
            // numStart
            // 
            this.numStart.Location = new System.Drawing.Point(13, 125);
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(120, 31);
            this.numStart.TabIndex = 2;
            this.numStart.ValueChanged += new System.EventHandler(this.numStart_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(494, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "с какой строки Excel начинать печать (минимум 1)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(394, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "по какую строку печатать (например, 30)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // numEnd
            // 
            this.numEnd.Location = new System.Drawing.Point(13, 162);
            this.numEnd.Name = "numEnd";
            this.numEnd.Size = new System.Drawing.Size(120, 31);
            this.numEnd.TabIndex = 4;
            this.numEnd.ValueChanged += new System.EventHandler(this.numEnd_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(494, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "номер самого студенческого билета (например, 256)";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // numCardStart
            // 
            this.numCardStart.Location = new System.Drawing.Point(12, 199);
            this.numCardStart.Name = "numCardStart";
            this.numCardStart.Size = new System.Drawing.Size(120, 31);
            this.numCardStart.TabIndex = 6;
            this.numCardStart.ValueChanged += new System.EventHandler(this.numCardStart_ValueChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Unispace", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(18, 256);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(188, 54);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "{ПЕЧАТЬ}";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click_1);
            // 
            // txtTemplate
            // 
            this.txtTemplate.Location = new System.Drawing.Point(18, 74);
            this.txtTemplate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(246, 31);
            this.txtTemplate.TabIndex = 1;
            this.txtTemplate.Text = "Сюда Путь К Шаблону";
            this.txtTemplate.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 485);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numCardStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numEnd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numStart);
            this.Controls.Add(this.txtTemplate);
            this.Controls.Add(this.txtExcel);
            this.Font = new System.Drawing.Font("Unispace", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "CardMakerV1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCardStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExcel;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCardStart;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtTemplate;
    }
}

