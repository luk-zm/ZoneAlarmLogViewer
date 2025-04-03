﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace import_danych
{
    internal class fileProcessing
    {
        public enum LINE_RESULT
        {
            BAD_NUM_OF_ELEMENTS,
            BAD_TYPE,
            OK
        }

        static public LINE_RESULT validateLine(string line)
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
            if (first_element == "type")
                return LINE_RESULT.BAD_TYPE;
            return LINE_RESULT.OK;
        }

        static public string[] extractLineElements(string line)
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

        static public void saveToDatabase(string type, string date, string time, string inAddr,
            string outAddr, string protcol, SqlConnection connection)
        {
            if (connection == null)
            {
                return;
            }
            string commandText = "Insert into ZoneAlarmLog(Zdarzenie, Data, Czas, Source, Destination, Transport)" +
                "values('" + type + "','" + date + "','" + time + "','" + inAddr + "','" + outAddr + "','" + protcol + "');";
            SqlCommand command = new SqlCommand(commandText, connection);
            try
            {
                int res = command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Sql command failure");
            }
        }

        static public(List<string>[] data, int processedLinesCount) processFile(string fileName, SqlConnection connection = null)
        {
            List<string>[] processedData = new List<string>[8];
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
                            processedLinesCount++;
                            if (connection != null && connection.State == System.Data.ConnectionState.Open)
                            {
                                saveToDatabase(elements[0], elements[1], elements[2],
                                    elements[3], elements[4], elements[5], connection);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exp.Message);
            }
            processedData[7].Add(fileName);
            return (processedData, processedLinesCount);
        }

        static public void processFolderThread(ref string[] files, int startIdx, int endIdx, ref List<string>[] result, ref int processedLinesCount, SqlConnection connection = null)
        {
            result = new List<string>[8];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = new List<string>();
            }
            for (; startIdx < endIdx; ++startIdx)
            {
                if (!files[startIdx].EndsWith(".txt"))
                    continue;
                (List<string>[] processingResult, int processedLinesOfFileCount) = processFile(files[startIdx], connection);
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

        static public (List<string>[] data, int processedLinesCount) processFiles(string[] files, SqlConnection connection = null)
        {
            List<string>[] result = new List<string>[8];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = new List<string>();
            }

            int numOfThreads = 6;
            Thread[] threads = new Thread[numOfThreads];
            List<string>[][] tempResults = new List<string>[numOfThreads][];
            int[] processedLinesCounts = new int[numOfThreads];

            int step = (int)Math.Ceiling((double)(files.Length) / numOfThreads);

            for (int i = 0; i < numOfThreads - 1; ++i)
            {
                int threadIndex = i; // C# lambda capture
                threads[threadIndex] = new Thread(() => processFolderThread(ref files, step * threadIndex,
                    step * (threadIndex + 1), ref tempResults[threadIndex], ref processedLinesCounts[threadIndex], connection));
                threads[threadIndex].Start();
            }
            threads[numOfThreads - 1] = new Thread(() =>
                processFolderThread(ref files, (numOfThreads - 1) * step, files.Length,
                ref tempResults[numOfThreads - 1], ref processedLinesCounts[numOfThreads - 1], connection));
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

        static public(List<string>[] data, int processedLinesCount) processFolder(string folderName, SqlConnection connection = null)
        {
            string[] files = Directory.GetFiles(folderName);

            return processFiles(files, connection);
        }
    }
}
