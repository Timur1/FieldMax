namespace FieldMaxTest
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button_Start = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button_average = new System.Windows.Forms.Button();
            this.numericUpDown_amountOfAverDots = new System.Windows.Forms.NumericUpDown();
            this.label_amountOfAverDots = new System.Windows.Forms.Label();
            this.label_AveragePower = new System.Windows.Forms.Label();
            this.textBoxAveragePower = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ZeroButton = new System.Windows.Forms.Button();
            this.button_ExportData = new System.Windows.Forms.Button();
            this.button_browse = new System.Windows.Forms.Button();
            this.textBox_FileName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_amountOfAverDots)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(272, 483);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(596, 82);
            this.listBox1.TabIndex = 2;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.IsShowPointValues = false;
            this.zedGraphControl1.Location = new System.Drawing.Point(272, 29);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(2);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.PointValueFormat = "G";
            this.zedGraphControl1.Size = new System.Drawing.Size(596, 449);
            this.zedGraphControl1.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(52, 29);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(188, 49);
            this.button_Start.TabIndex = 4;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Clicked);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(52, 100);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(188, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "BackLight";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BackLight_Click);
            // 
            // button_average
            // 
            this.button_average.Location = new System.Drawing.Point(272, 628);
            this.button_average.Name = "button_average";
            this.button_average.Size = new System.Drawing.Size(75, 23);
            this.button_average.TabIndex = 7;
            this.button_average.Text = "Get Average";
            this.button_average.UseVisualStyleBackColor = true;
            this.button_average.Click += new System.EventHandler(this.button_average_Click);
            // 
            // numericUpDown_amountOfAverDots
            // 
            this.numericUpDown_amountOfAverDots.Location = new System.Drawing.Point(353, 628);
            this.numericUpDown_amountOfAverDots.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_amountOfAverDots.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_amountOfAverDots.Name = "numericUpDown_amountOfAverDots";
            this.numericUpDown_amountOfAverDots.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_amountOfAverDots.TabIndex = 8;
            this.numericUpDown_amountOfAverDots.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label_amountOfAverDots
            // 
            this.label_amountOfAverDots.AutoSize = true;
            this.label_amountOfAverDots.Location = new System.Drawing.Point(366, 612);
            this.label_amountOfAverDots.Name = "label_amountOfAverDots";
            this.label_amountOfAverDots.Size = new System.Drawing.Size(82, 13);
            this.label_amountOfAverDots.TabIndex = 9;
            this.label_amountOfAverDots.Text = "Amount Of Dots";
            // 
            // label_AveragePower
            // 
            this.label_AveragePower.AutoSize = true;
            this.label_AveragePower.Location = new System.Drawing.Point(511, 611);
            this.label_AveragePower.Name = "label_AveragePower";
            this.label_AveragePower.Size = new System.Drawing.Size(80, 13);
            this.label_AveragePower.TabIndex = 10;
            this.label_AveragePower.Text = "Average Power";
            // 
            // textBoxAveragePower
            // 
            this.textBoxAveragePower.Location = new System.Drawing.Point(504, 627);
            this.textBoxAveragePower.Name = "textBoxAveragePower";
            this.textBoxAveragePower.Size = new System.Drawing.Size(249, 20);
            this.textBoxAveragePower.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ZeroButton);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(52, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 168);
            this.panel1.TabIndex = 12;
            // 
            // ZeroButton
            // 
            this.ZeroButton.Location = new System.Drawing.Point(3, 60);
            this.ZeroButton.Name = "ZeroButton";
            this.ZeroButton.Size = new System.Drawing.Size(75, 22);
            this.ZeroButton.TabIndex = 0;
            this.ZeroButton.Text = "Zero";
            this.ZeroButton.UseVisualStyleBackColor = true;
            this.ZeroButton.Click += new System.EventHandler(this.ZeroButton_Click);
            // 
            // button_ExportData
            // 
            this.button_ExportData.Location = new System.Drawing.Point(111, 414);
            this.button_ExportData.Name = "button_ExportData";
            this.button_ExportData.Size = new System.Drawing.Size(122, 38);
            this.button_ExportData.TabIndex = 13;
            this.button_ExportData.Text = "Export";
            this.button_ExportData.UseVisualStyleBackColor = true;
            this.button_ExportData.Click += new System.EventHandler(this.button_ExportData_Click);
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(158, 385);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(75, 23);
            this.button_browse.TabIndex = 14;
            this.button_browse.Text = "Browse";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // textBox_FileName
            // 
            this.textBox_FileName.Location = new System.Drawing.Point(52, 388);
            this.textBox_FileName.Name = "textBox_FileName";
            this.textBox_FileName.Size = new System.Drawing.Size(100, 20);
            this.textBox_FileName.TabIndex = 15;
            this.textBox_FileName.Text = "data.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 721);
            this.Controls.Add(this.textBox_FileName);
            this.Controls.Add(this.button_browse);
            this.Controls.Add(this.button_ExportData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxAveragePower);
            this.Controls.Add(this.label_AveragePower);
            this.Controls.Add(this.label_amountOfAverDots);
            this.Controls.Add(this.numericUpDown_amountOfAverDots);
            this.Controls.Add(this.button_average);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_amountOfAverDots)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_average;
        private System.Windows.Forms.NumericUpDown numericUpDown_amountOfAverDots;
        private System.Windows.Forms.Label label_amountOfAverDots;
        private System.Windows.Forms.Label label_AveragePower;
        private System.Windows.Forms.TextBox textBoxAveragePower;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ZeroButton;
        private System.Windows.Forms.Button button_ExportData;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.TextBox textBox_FileName;
    }
}

