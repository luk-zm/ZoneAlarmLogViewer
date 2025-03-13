using System;
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

        public List<string>[] processFile(string fileName)
        {
            List<string>[] processedData = new List<string>[7];
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
                        processedData[0].Add(line);
                        string[] elements = extractLineElements(line);
                        if (elements != null)
                        {
                            processedData[1].Add(elements[0]);
                            processedData[2].Add(elements[1]);
                            processedData[3].Add(elements[2]);
                            processedData[4].Add(elements[3]);
                            processedData[5].Add(elements[4]);
                            processedData[6].Add(elements[5]);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exp.Message);
            }
            return processedData;
        }

        public void appendViews(List<string>[] data)
        {
            for (int i = 0; i < data[0].Count; ++i)
            {
                fullFileListBox.Items.Add(data[0][i]);
            }
            for (int i = 0; i < data[1].Count; ++i)
            {
                col1ListBox.Items.Add(data[1][i]);
                col2ListBox.Items.Add(data[2][i]);
                col3ListBox.Items.Add(data[3][i]);
                col4ListBox.Items.Add(data[4][i]);
                col5ListBox.Items.Add(data[5][i]);
                col6ListBox.Items.Add(data[6][i]);
            }
        }

        public void updateViews(List<string>[] data)
        {
            /*
            fullFileListBox.BeginUpdate();
            col1ListBox.BeginUpdate();
            col2ListBox.BeginUpdate();
            col3ListBox.BeginUpdate();
            col4ListBox.BeginUpdate();
            col5ListBox.BeginUpdate();
            col6ListBox.BeginUpdate();

            fullFileListBox.DataSource = data[0];
            col1ListBox.DataSource = data[1];
            col2ListBox.DataSource = data[2];
            col3ListBox.DataSource = data[3];
            col4ListBox.DataSource = data[4];
            col5ListBox.DataSource = data[5];
            col6ListBox.DataSource = data[6];

            fullFileListBox.EndUpdate();
            col1ListBox.EndUpdate();
            col2ListBox.EndUpdate();
            col3ListBox.EndUpdate();
            col4ListBox.EndUpdate();
            col5ListBox.EndUpdate();
            col6ListBox.EndUpdate();
            */
            fullFileListBox.BeginUpdate();
            col1ListBox.BeginUpdate();
            col2ListBox.BeginUpdate();
            col3ListBox.BeginUpdate();
            col4ListBox.BeginUpdate();
            col5ListBox.BeginUpdate();
            col6ListBox.BeginUpdate();
            fullFileListBox.Items.Clear();
            col1ListBox.Items.Clear();
            col2ListBox.Items.Clear();
            col3ListBox.Items.Clear();
            col4ListBox.Items.Clear();
            col5ListBox.Items.Clear();
            col6ListBox.Items.Clear();
            for (int i = 0; i < data[0].Count; ++i)
            {
                fullFileListBox.Items.Add(data[0][i]);
            }
            for (int i = 0; i < data[1].Count; ++i)
            {
                col1ListBox.Items.Add(data[1][i]);
                col2ListBox.Items.Add(data[2][i]);
                col3ListBox.Items.Add(data[3][i]);
                col4ListBox.Items.Add(data[4][i]);
                col5ListBox.Items.Add(data[5][i]);
                col6ListBox.Items.Add(data[6][i]);
            }
            fullFileListBox.EndUpdate();
            col1ListBox.EndUpdate();
            col2ListBox.EndUpdate();
            col3ListBox.EndUpdate();
            col4ListBox.EndUpdate();
            col5ListBox.EndUpdate();
            col6ListBox.EndUpdate();
        }
        public Form1()
        {
            InitializeComponent();
            /*
            fullFileListBox.VirtualMode = true;
            fullFileListBox.VirtualListSize = 10000;
            col1ListBox.VirtualMode = true;
            col1ListBox.VirtualListSize = 10000;
            col2ListBox.VirtualMode = true;
            col2ListBox.VirtualListSize = 10000;
            col3ListBox.VirtualMode = true;
            col3ListBox.VirtualListSize = 10000;
            col4ListBox.VirtualMode = true;
            col4ListBox.VirtualListSize = 10000;
            col5ListBox.VirtualMode = true;
            col5ListBox.VirtualListSize = 10000;
            col6ListBox.VirtualMode = true;
            col6ListBox.VirtualListSize = 10000;


            fullFileListBox.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(fullFileListBox_RetrieveVirtualItem);
            fullFileListBox.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            col1ListBox.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            col1ListBox.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            col2ListBox.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            col2ListBox.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            col3ListBox.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            col3ListBox.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            col4ListBox.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            col4ListBox.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            col5ListBox.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            col5ListBox.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            col6ListBox.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            col6ListBox.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            */
        }

        public ListViewItem[] cache;

        void fullFileListBox_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {

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
            List<string>[] data = processFile(fileName);
            updateViews(data);
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

        public void processFolderThread(ref string[] files, int startIdx, int endIdx, ref List<string>[] result)
        {
            result = new List<string>[7];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = new List<string>();
            }
            for (; startIdx < endIdx; ++startIdx)
            {
                List<string>[] processingResult = processFile(files[startIdx]);
                for (int j = 0; j < result.Length; ++j)
                {
                    for (int i = 0; i < processingResult[j].Count; ++i)
                    {
                        result[j].Add(processingResult[j][i]);
                    }
                }
            }
        }

        public List<string>[] processFolder(string folderName)
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
            
            int step = (int)Math.Ceiling((double)(files.Length) / numOfThreads);

            for (int i = 0; i < numOfThreads - 1; ++i)
            {
                int threadIndex = i; // C# lambda capture
                threads[threadIndex] = new Thread(() => processFolderThread(ref files, step * threadIndex,
                    step * (threadIndex + 1), ref tempResults[threadIndex]));
                threads[threadIndex].Start();
            }
            threads[numOfThreads - 1] = new Thread(() =>
                processFolderThread(ref files, (numOfThreads - 1)*step, files.Length, ref tempResults[numOfThreads - 1]));
            threads[numOfThreads - 1].Start();

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
            }
            return result;
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
                List<string>[] data = processFolder(folderName);
               // data[0].CopyTo(cache.ToArray());
                ts = s.Elapsed;
                Console.WriteLine(ts);
                updateViews(data);
                ts = s.Elapsed - ts;
                Console.WriteLine(ts);
            }
        }
    }
}
