using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace import_danych
{
    public partial class Form1 : Form
    {



        public void updateViews(List<string>[] data)
        {
        }
        public Form1()
        {
            InitializeComponent();

            dataListView.VirtualListSize = 0;
            dataListView.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_RetrieveVirtualItem);

            allLinesListView.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(fullFileListView_RetrieveVirtualItem);
            data = new List<string>[8];
        }

        public List<string>[] data;
        SqlConnection connection;

        void openConnection()
        {
            string connectionString = "Data Source=(local);Initial Catalog=HurtowniaDanych;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem item;
            if (data != null)
            {
                item = new ListViewItem(data[1][e.ItemIndex]);
                item.SubItems.Add(data[2][e.ItemIndex]);
                item.SubItems.Add(data[3][e.ItemIndex]);
                item.SubItems.Add(data[4][e.ItemIndex]);
                item.SubItems.Add(data[5][e.ItemIndex]);
                item.SubItems.Add(data[6][e.ItemIndex]);
            }
            else
            {
                item = new ListViewItem("N/A");
                item.SubItems.Add("N/A");
                item.SubItems.Add("N/A");
                item.SubItems.Add("N/A");
                item.SubItems.Add("N/A");
                item.SubItems.Add("N/A");
            }
            e.Item = item;
        }

        void fullFileListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (data != null)
            {
                e.Item = new ListViewItem(data[0][e.ItemIndex]);
            }
            else
            {
                e.Item = new ListViewItem("N/A");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fileLoadButton_Click(object sender, EventArgs e)
        {
            openConnection();
            string fileName = fileNameTextBox.Text;
            (List<string>[] data, int processedLinesCount) = fileProcessing.processFile(fileName);
            this.data = data;
            processedLinesCountLabel.Text = "Przetworzone linijki: " + processedLinesCount;
            dataListView.VirtualListSize = data[1].Count;
            allLinesListView.VirtualListSize = data[0].Count;
            connection.Close();
        }

        private void fileDialogButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileNameTextBox.Text = openFileDialog.FileName;
            }
        }

        private void aboutProgramButton_Click(object sender, EventArgs e)
        {
            Form aboutForm = new aboutProgramForm();
            aboutForm.Show(this);
        }



        private void openCatalogButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            //folderBrowserDialog.SelectedPath = @"D:\rizzai\ZoneAlarmLogViewer\ZoneAlarmLogViewer\dane";
            folderBrowserDialog.SelectedPath = @"C:\Users\Luk\source\repos\import_danych\ZoneAlarmLogViewer\dane\db";
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                string folderName = folderBrowserDialog.SelectedPath;

                processFileForm processFileForm = new processFileForm(folderName, ref this.data,
                    ref dataListView, ref allLinesListView, ref processedLinesCountLabel, connection);
                processFileForm.Show(this);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
        }
    }
}
