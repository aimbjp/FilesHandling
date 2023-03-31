using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace FilesHandling
{
    public partial class Form1 : Form
    {
        private bool flagPathChosen = false;

        NoLinqSolver _noLinqSolver = new NoLinqSolver();
        LinqSolver _linqSolver = new LinqSolver();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void folderChooseBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
            var path = folderBrowserDialog1.SelectedPath;
            folderPathText.Text = path;
            flagPathChosen = true;
        }

        [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH", MessageId = "type: System.String; size: 77MB")]
        private void submit_Click(object sender, EventArgs e)
        {
            if (!flagPathChosen)
            {
                fileExistLabel.Text = "Пожалуйста выберите файл";
                return;
            }
            _linqSolver = new LinqSolver(folderPathText.Text, (int)amountFiles.Value,  (int)interval.Value);
            _noLinqSolver = new NoLinqSolver(folderPathText.Text, (int)amountFiles.Value,  (int)interval.Value);
            fileExistLabel.ForeColor = Color.Red;
            fileExistLabel.Text = _noLinqSolver.flagFileNotExist ? "Файлов не существует в выбранной папке" : "Файлы были найдены";
            FillDataGridView(_noLinqSolver, _linqSolver);
            FillChart();
        }

        private void FillDataGridView(NoLinqSolver _noLinqSolver, LinqSolver _linqSolver)
        {
            grid1.Rows.Clear();
            grid1.RowCount = 5;
            grid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            grid1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid1[0, 0].Value = "Саммое длинное имя файла";
            grid1[0, 1].Value = amountFiles.Value + " файлов с наибольшим весом";
            grid1[0, 2].Value = "Наибольшая группа файлов созданная в интервалe " + interval.Value + " минут";
            grid1[0, 3].Value = "Файлы распределенные по расширениям";
            grid1[0, 4].Value = "Сумма";
            for (var i = 0; i < 4; i++)
            {
                grid1[1, i].Value = _noLinqSolver.time[i];
                grid1[2, i].Value = _linqSolver.time[i];
            }
            grid1[1, 4].Value = _noLinqSolver.time.Sum();
            grid1[2, 4].Value = _linqSolver.time.Sum();
            grid1[3, 0].Value = _linqSolver.LongestFileName;
            grid1[3, 1].Value = _linqSolver.BiggestFiles;
            grid1[3, 2].Value = _noLinqSolver.BiggestGroupsFiles;
            grid1[3, 3].Value = _linqSolver.FilesGroupedExtension;
        }

        private void FillChart()
        {
            chart1.Series[0].Points.Clear();
            //extra task
            chart1.Series[1].Points.Clear();
            for (var i = 0; i < grid1.RowCount * 2 - 2; i++)
            {
                //extra task
                chart1.Series[i % 2].Points.AddXY(
                    grid1[0, 
                        i % 2 == 1 
                        ? (i - 1) / 2
                        : i  / 2].Value, //+ (i % 2 == 1 ? "WithLinq" : "NoLinq"), 
                    grid1[i % 2 + 1, 
                        i % 2 == 1 
                        ? (i - 1) / 2
                        : i  / 2].Value);
                
            }
            //extra task
            chart2.Series[0].Points.Clear();
            var k = 1;
            foreach (var group in _noLinqSolver.filesGrouped)
            {
                chart2.Series[0].Points.AddXY(group.Key, group.Value.Trim(' ').Split(',').Length);
            }
        }
    }
}