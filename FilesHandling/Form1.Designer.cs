namespace FilesHandling
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.folderPathText = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderChooseBtn = new System.Windows.Forms.Button();
            this.testText = new System.Windows.Forms.RichTextBox();
            this.submit = new System.Windows.Forms.Button();
            this.amountFiles = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.interval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amountFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 315);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(930, 559);
            this.dataGridView1.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(959, 556);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(420, 318);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите файл";
            // 
            // folderPathText
            // 
            this.folderPathText.AcceptsReturn = true;
            this.folderPathText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.folderPathText.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.folderPathText.Location = new System.Drawing.Point(21, 51);
            this.folderPathText.Multiline = true;
            this.folderPathText.Name = "folderPathText";
            this.folderPathText.ReadOnly = true;
            this.folderPathText.Size = new System.Drawing.Size(318, 154);
            this.folderPathText.TabIndex = 4;
            this.folderPathText.Text = "Path...";
            // 
            // folderChooseBtn
            // 
            this.folderChooseBtn.Location = new System.Drawing.Point(202, 211);
            this.folderChooseBtn.Name = "folderChooseBtn";
            this.folderChooseBtn.Size = new System.Drawing.Size(137, 28);
            this.folderChooseBtn.TabIndex = 5;
            this.folderChooseBtn.Text = "Выбрать папку";
            this.folderChooseBtn.UseVisualStyleBackColor = true;
            this.folderChooseBtn.Click += new System.EventHandler(this.folderChooseBtn_Click);
            // 
            // testText
            // 
            this.testText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testText.Location = new System.Drawing.Point(21, 259);
            this.testText.Name = "testText";
            this.testText.Size = new System.Drawing.Size(919, 607);
            this.testText.TabIndex = 6;
            this.testText.Text = "";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(1206, 36);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(172, 45);
            this.submit.TabIndex = 7;
            this.submit.Text = "Выполнить";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // amountFiles
            // 
            this.amountFiles.Location = new System.Drawing.Point(755, 51);
            this.amountFiles.Name = "amountFiles";
            this.amountFiles.Size = new System.Drawing.Size(126, 30);
            this.amountFiles.TabIndex = 9;
            this.amountFiles.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(755, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "Кол-во файлов";
            // 
            // interval
            // 
            this.interval.Location = new System.Drawing.Point(755, 169);
            this.interval.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(126, 30);
            this.interval.TabIndex = 11;
            this.interval.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(755, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 49);
            this.label3.TabIndex = 12;
            this.label3.Text = "Кол-во минут (интервал)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1391, 878);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.interval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.amountFiles);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.testText);
            this.Controls.Add(this.folderChooseBtn);
            this.Controls.Add(this.folderPathText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amountFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.NumericUpDown interval;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.NumericUpDown amountFiles;

        private System.Windows.Forms.Button submit;

        private System.Windows.Forms.RichTextBox testText;

        private System.Windows.Forms.Button folderChooseBtn;

        private System.Windows.Forms.TextBox folderPathText;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}