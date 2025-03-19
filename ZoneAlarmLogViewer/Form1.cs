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
    public partial class Form1: Form
    {

        public enum LINE_RESULT
        {
            BAD_NUM_OF_ELEMENTS,
            BAD_TYPE,
            OK
        }

        public LINE_RESULT validateLine(string line)
        {
            int commaCount = 0;
            for (int i = 0; i < line.Length; ++i)
            {
                if (line[i] == ',')
                    commaCount++;
            }
            if (commaCount != 5)
                return LINE_RESULT.BAD_NUM_OF_ELEMENTS;
            int firstCommaIndex = line.IndexOf(',');
            string first_element = line.Substring(0, firstCommaIndex);
            if (first_element != "FWIN" && first_element != "PE" && first_element != "ZLUpdate")
                return LINE_RESULT.BAD_TYPE;
            return LINE_RESULT.OK;
        }

        public string[] extractLineElements(string line)
        {
            if (validateLine(line) != LINE_RESULT.OK)
            {
                return null;
            }

            string[] result = new string[6];
            int resultIdx = 0;
            int elementIdx = 0;
            int commaIdx = line.IndexOf(',');
            while (commaIdx > -1)
            {
                result[resultIdx++] = line.Substring(elementIdx, commaIdx - elementIdx);
                elementIdx = commaIdx + 1;
                commaIdx = line.IndexOf(',', elementIdx);
            }
            result[resultIdx] = line.Substring(elementIdx);
            return result;
        }

        public (List<string>[] data, int processedLinesCount) processFile(string fileName)
        {
            List<string>[] processedData = new List<string>[7];
            int processedLinesCount = 0;
            for (int i = 0; i < processedData.Length; ++i)
            {
                processedData[i] = new List<string>();
            }
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] elements = extractLineElements(line);
                        if (elements != null)
                        {
                            processedData[1].Add(elements[0]);
                            processedData[2].Add(elements[1]);
                            processedData[3].Add(elements[2]);
                            processedData[4].Add(elements[3]);
                            processedData[5].Add(elements[4]);
                            processedData[6].Add(elements[5]);
                            processedLinesCount++;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exp.Message);
            }
            processedData[0].Add(fileName);
            return (processedData, processedLinesCount);
        }

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
            (List<string>[] data, int processedLinesCount) = processFile(fileName);
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

        public void processFolderThread(ref string[] files, int startIdx, int endIdx, ref List<string>[] result, ref int processedLinesCount)
        {
            result = new List<string>[7];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = new List<string>();
            }
            for (; startIdx < endIdx; ++startIdx)
            {
                if (!files[startIdx].EndsWith(".txt"))
                    continue;
                (List<string>[] processingResult, int processedLinesOfFileCount) = processFile(files[startIdx]);
                processedLinesCount += processedLinesOfFileCount;
                for (int j = 0; j < result.Length; ++j)
                {
                    for (int i = 0; i < processingResult[j].Count; ++i)
                    {
                        result[j].Add(processingResult[j][i]);
                    }
                }
            }
        }

        public (List<string>[] data, int processedLinesCount) processFolder(string folderName)
        {
            List<string>[] result = new List<string>[7];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = new List<string>();
            }
            string[] files = Directory.GetFiles(folderName);
            
            int numOfThreads = 6;
            Thread[] threads = new Thread[numOfThreads];
            List<string>[][] tempResults = new List<string>[numOfThreads][];
            int[] processedLinesCounts = new int[numOfThreads];
            
            int step = (int)Math.Ceiling((double)(files.Length) / numOfThreads);

            for (int i = 0; i < numOfThreads - 1; ++i)
            {
                int threadIndex = i; // C# lambda capture
                threads[threadIndex] = new Thread(() => processFolderThread(ref files, step * threadIndex,
                    step * (threadIndex + 1), ref tempResults[threadIndex], ref processedLinesCounts[threadIndex]));
                threads[threadIndex].Start();
            }
            threads[numOfThreads - 1] = new Thread(() =>
                processFolderThread(ref files, (numOfThreads - 1)*step, files.Length,
                ref tempResults[numOfThreads - 1], ref processedLinesCounts[numOfThreads - 1]));
            threads[numOfThreads - 1].Start();

            int processedLinesCount = 0;
            for (int i = 0; i < numOfThreads; ++i)
            {
                threads[i].Join();
            }
            for (int i = 0; i < numOfThreads; ++i)
            {
                for (int dataCol = 0; dataCol < result.Length; ++dataCol)
                {
                    for (int j = 0; j < tempResults[i][dataCol].Count; ++j)
                        result[dataCol].Add(tempResults[i][dataCol][j]);
                }
                processedLinesCount += processedLinesCounts[i];
            }
            return (result, processedLinesCount);
        }

        private void openCatalogButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = "C:\\Users\\Luk\\source\\repos\\import_danych\\import_danych\\dane\\";
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                string folderName = folderBrowserDialog.SelectedPath;
                /*string[] files = Directory.GetFiles(folderName);

                foreach(string file in files)
                {
                    List<string>[] data1 = processFile(file);
                    appendViews(data1);
                }*/

                TimeSpan ts;
                Stopwatch s = Stopwatch.StartNew();
                var bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = true;

                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.RunWorkerAsync(folderName);

                /*(List<string>[] data, int processedLinesCount) = processFolder(folderName);
                processedLinesCountLabel.Text = "Przetworzone linijki: " + processedLinesCount;
                this.data = data;
                ts = s.Elapsed;
                Console.WriteLine(ts);
                listView.VirtualListSize = data[1].Count;
                listView.Refresh();
                fullFileListView.VirtualListSize = data[0].Count;
                fullFileListView.Refresh();
                ts = s.Elapsed - ts;
                Console.WriteLine(ts);*/
            }
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            
            string folderName = (string)e.Argument;
            e.Result = processFolder(folderName);

        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("anulowano");
                return;
            }
            var (data, processedLinesCount) = ((List<string>[], int))e.Result;
            this.data = data;
            processedLinesCountLabel.Text = "Przetworzone linijki: " + processedLinesCount;
            listView.VirtualListSize = this.data[1].Count;
            listView.Refresh();
            fullFileListView.VirtualListSize = this.data[0].Count;
            fullFileListView.Refresh();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
