using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace import_danych
{
    public partial class processFileForm : Form
    {
        public string folderName;
        public List<string>[] data;
        public System.Windows.Forms.ListView dataListView;
        public System.Windows.Forms.ListView allLinesListView;
        Label processedLinesCountLabel;
        SqlConnection connection;
        public processFileForm(string folderName, ref List<string>[] data, ref System.Windows.Forms.ListView dataListView, 
            ref System.Windows.Forms.ListView allLinesListView, ref Label processedLinesCountLabel, SqlConnection connection)
        {
            InitializeComponent();
            this.folderName = folderName;
            this.data = data;
            this.dataListView = dataListView;
            this.allLinesListView = allLinesListView;
            this.processedLinesCountLabel = processedLinesCountLabel;
            processedFilesListView.VirtualMode = true;
            processedFilesListView.VirtualListSize = 0;
            processedFilesListView.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_RetrieveVirtualItem);
            this.connection = connection;
        }
        void openConnection()
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=HurtowniaDanych;Trusted_Connection=True;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (data != null)
            {
                e.Item = new ListViewItem(data[7][e.ItemIndex]);
            }
            else
            {
                e.Item = new ListViewItem("N/A");
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        BackgroundWorker bw;

        private void startButton_Click(object sender, EventArgs e)
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerAsync((folderName, bw));
            startButton.Enabled = false;
        }

        static string[] subarray(string[] arr, int startIndex, int endIndex)
        {
            string[] res = new string[endIndex - startIndex];
            for (int i = 0; i < res.Length; ++i)
            {
                res[i] = arr[startIndex + i];
            }
            return res;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var (folderName, bw) = ((string, BackgroundWorker))e.Argument;
            string[] files = Directory.GetFiles(folderName);
            int step = files.Length / 10;
            List<string>[] resultData = new List<string>[8];
            for (int i = 0; i < resultData.Length; ++i)
            {
                resultData[i] = new List<string>();
            }
            int linesCount = 0;
            for (int i = 0; i < 10; ++i)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                string[] fileChunk = subarray(files, step * i, step * (i + 1));
                var (tmpData, tmpLinesCount) = fileProcessing.processFiles(fileChunk);
                for (int j = 0; j  < tmpData.Length; ++j)
                {
                    for (int k = 0; k < tmpData[j].Count; ++k)
                    {
                        resultData[j].Add(tmpData[j][k]);
                    }
                }
                linesCount += tmpLinesCount;
                bw.ReportProgress((i+1)*10);
            }
            string[] lastFileChunk = subarray(files, step * 10, files.Length);
            var (lastTmpData, lastTmpLinesCount) = fileProcessing.processFiles(lastFileChunk);
            for (int j = 0; j < lastTmpData.Length; ++j)
            {
                for (int k = 0; k < lastTmpData[j].Count; ++k)
                {
                    resultData[j].Add(lastTmpData[j][k]);
                }
            }
            linesCount += lastTmpLinesCount;
            e.Result = (resultData, linesCount);
        }
        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Anulowano");
                return;
            }
            var (data, processedLinesCount) = ((List<string>[], int))e.Result;
            data.CopyTo(this.data, 0);
            processedFilesListView.VirtualListSize = data[7].Count;
            allLinesListView.VirtualListSize = data[0].Count;
            dataListView.VirtualListSize = data[1].Count;
            processedLinesCountLabel.Text = "Przetworzone linijki: " + processedLinesCount;
            startButton.Enabled = true;
            openConnection();
            if (connection.State != ConnectionState.Executing)
            {
                new Thread(() =>
                {
                    for (int i = 0; i < data[1].Count; ++i)
                    {
                        if (connection.State == ConnectionState.Closed)
                            openConnection();
                        fileProcessing.saveToDatabase(data[1][i], data[2][i], data[3][i], data[4][i], data[5][i], data[6][i], connection);
                    }
                    connection.Close();
                }).Start();
            }
            else
            {
                MessageBox.Show("Baza danych zajęta");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
                startButton.Enabled = true; ;
            }
        }
    }
}
