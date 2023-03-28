using System;
using System.Windows.Forms;


namespace FilesHandling
{
    public partial class Form1 : Form
    {
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
        }

        private void submit_Click(object sender, EventArgs e)
        {
            testText.Text = "Длинное имя nolinq:  " + _noLinqSolver.FindLongestFileName(folderPathText.Text) + "\n" +
                            "Длинное имя Linq:  " +
                            _linqSolver.FindLongestFileName(folderPathText.Text) + "\n" + "Наибольший вес no linq: " +
                            _noLinqSolver.FindBiggestFiles(folderPathText.Text, (int)amountFiles.Value) + "\n" + 
                            "Biggest files linq: " + _linqSolver.FindBiggestFiles(folderPathText.Text, (int)amountFiles.Value) + "\n" +
                            "Biggest groups files NOlinq:" + _noLinqSolver.FindBiggestGroupsFiles(folderPathText.Text, (int)interval.Value) + 
                            "\n" + "Biggest groups files linq:" + _linqSolver.FindBiggestGroupsFiles(folderPathText.Text, (int)interval.Value) +
                            "\n" + "alloc files by extension NOlinq" + _noLinqSolver.AllocationFilesExtension(folderPathText.Text) + "\n" +
                            "alloc files by extension linq" + _linqSolver.AllocationFilesExtension(folderPathText.Text);
            
        }
    }
}