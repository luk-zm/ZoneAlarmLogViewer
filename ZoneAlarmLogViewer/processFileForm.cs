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
    public partial class processFileForm : Form
    {
        public string folderName;
        public List<string>[] data;
        public System.Windows.Forms.ListView dataListView;
        public System.Windows.Forms.ListView allLinesListView;
        Label processedLinesCountLabel;
        public processFileForm(string folderName, ref List<string>[] data, ref System.Windows.Forms.ListView dataListView, 
            ref System.Windows.Forms.ListView allLinesListView, ref Label processedLinesCountLabel)
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

        private void startButton_Click(object sender, EventArgs e)
        {
            var bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync(folderName);
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            string folderName = (string)e.Argument;
            e.Result = fileProcessing.processFolder(folderName);

        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("anulowano");
                return;
            }
            var (data, processedLinesCount) = ((List<string>[], int))e.Result;
            data.CopyTo(this.data, 0);
            processedFilesListView.VirtualListSize = data[7].Count;
            allLinesListView.VirtualListSize = data[0].Count;
            dataListView.VirtualListSize = data[1].Count;
            processedLinesCountLabel.Text = "Przetworzone linijki: " + processedLinesCount;
        }
    }
}
