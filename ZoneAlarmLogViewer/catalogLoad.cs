using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace import_danych
{
    public partial class catalogLoad : Form
    {
        private string folderName;
        private List<string>[] data;
        public catalogLoad(string folderName, List<string>[] data)
        {
            InitializeComponent();
            this.folderName = folderName;
            this.data = data;
        }

        BackgroundWorker bw;

        private void startButton_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerAsync(folderName);
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            /*for (int i = 0; i < 10; ++i)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                string folderName = (string)e.Argument;
                e.Result = processFolder(folderName);
                bw.ReportProgress((i+1) * 10);
            }*/
            if (bw.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            string folderName = (string)e.Argument;
            e.Result = Form1.processFolder(folderName);
            bw.ReportProgress(100);
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Anulowano");
                return;
            }
            var (data, processedLinesCount) = ((List<string>[], int))e.Result;
            this.data = data;
            processedLinesCountLabel.Text = "Przetworzone linijki: " + processedLinesCount;
            listView.VirtualListSize = this.data[1].Count;
            listView.Refresh();
            fullFileListView.VirtualListSize = this.data[0].Count;
            fullFileListView.Refresh();
            openCatalogButton.Enabled = true;
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
                startButton.Enabled = true;
            }
        }
    }


}
