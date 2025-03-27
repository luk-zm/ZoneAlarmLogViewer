using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            listView.VirtualListSize = 0;
            listView.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_RetrieveVirtualItem);
            //listView.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView_CacheVirtualItems);

            fullFileListView.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(fullFileListView_RetrieveVirtualItem);
            //fullFileListView.CacheVirtualItems += new CacheVirtualItemsEventHandler(fullFileListView_CacheVirtualItems);
        }

        public List<string>[] data;
        public ListViewItem[] myCache;
        private CacheVirtualItemsEventArgs lastCacheArgsListView;
        public CacheVirtualItemsEventArgs lastCacheArgsFullListView;
        public int firstItem;

        void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (myCache != null && e.ItemIndex >= firstItem && e.ItemIndex < firstItem + myCache.Length)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                e.Item = myCache[e.ItemIndex - firstItem];
            }
            else
            {
                //A cache miss, so create a new ListViewItem and pass it back.
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
        }

        void listView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really neccesary.
            if (myCache != null && e.StartIndex >= firstItem && e.EndIndex <= firstItem + myCache.Length)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            firstItem = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1; //indexes are inclusive
            myCache = new ListViewItem[length];

            //Fill the cache with the appropriate ListViewItems.
            for (int i = 0; i < length; i++)
            {
                ListViewItem item;
                if (data != null)
                {
                    int targetIndex = e.StartIndex + i;
                    item = new ListViewItem(data[1][targetIndex]);
                    item.SubItems.Add(data[2][targetIndex]);
                    item.SubItems.Add(data[3][targetIndex]);
                    item.SubItems.Add(data[4][targetIndex]);
                    item.SubItems.Add(data[5][targetIndex]);
                    item.SubItems.Add(data[6][targetIndex]);
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
                myCache[i] = item;
            }
            lastCacheArgsListView = new CacheVirtualItemsEventArgs(e.StartIndex, e.EndIndex);
        }

        public ListViewItem[] fullFileViewCache;
        public int fullFileFirstItem;

        void fullFileListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (fullFileViewCache != null && e.ItemIndex >= fullFileFirstItem && e.ItemIndex < fullFileFirstItem + fullFileViewCache.Length)
            {
                e.Item = fullFileViewCache[e.ItemIndex - fullFileFirstItem];
            }
            else
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
        }

        void fullFileListView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really neccesary.
            if (fullFileViewCache != null && e.StartIndex >= fullFileFirstItem && e.EndIndex <= fullFileFirstItem + fullFileViewCache.Length)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //Now we need to rebuild the cache.
            fullFileFirstItem = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1; //indexes are inclusive
            fullFileViewCache = new ListViewItem[length];

            //Fill the cache with the appropriate ListViewItems.
            for (int i = 0; i < length; i++)
            {
                if (data != null)
                {
                    int targetIndex = e.StartIndex + i;
                    fullFileViewCache[i] = new ListViewItem(data[0][targetIndex]);
                }
                else
                {
                    fullFileViewCache[i] = new ListViewItem("N/A");
                }
            }
            lastCacheArgsFullListView = new CacheVirtualItemsEventArgs(e.StartIndex, e.EndIndex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fileLoadButton_Click(object sender, EventArgs e)
        {
            string fileName = fileNameTextBox.Text;
            (List<string>[] data, int processedLinesCount) = fileProcessing.processFile(fileName);
            this.data = data;
            processedLinesCountLabel.Text = "Przetworzone linijki: " + processedLinesCount;
            listView.VirtualListSize = data[1].Count;
            fullFileListView.VirtualListSize = data[0].Count;
            /*
            if (lastCacheArgsFullListView != null && lastCacheArgsListView != null)
            {
                fullFileListView_CacheVirtualItems(this, lastCacheArgsFullListView);
                listView_CacheVirtualItems(this, lastCacheArgsListView);
            }
            */
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
            folderBrowserDialog.SelectedPath = @"D:\rizzai\ZoneAlarmLogViewer\ZoneAlarmLogViewer\dane";
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                string folderName = folderBrowserDialog.SelectedPath;

                processFileForm processFileForm = new processFileForm(folderName);
                processFileForm.Show(this);

                this.data = processFileForm.data;
                int processedLinesCount = 0;
                processedLinesCountLabel.Text = "Przetworzone linijki: " + processedLinesCount;
                /*listView.VirtualListSize = this.data[1].Count;
                listView.Refresh();
                fullFileListView.VirtualListSize = this.data[0].Count;
                fullFileListView.Refresh();*/
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
