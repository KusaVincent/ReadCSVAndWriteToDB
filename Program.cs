using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ReadCSVAndWriteToDB
{
    internal class Program 
    {
        static void Main (string[] args)
        {
            String fileExt  = HandleConfigData.ConfigurationData("Authentication", "File:FileExt");
            String filePath = HandleConfigData.ConfigurationData("Authentication", "Directory:FileDir");

            // Get all CSV files in the directory
            DirectoryInfo dirInfo = new DirectoryInfo(filePath);
            FileInfo[] csvFiles = dirInfo.GetFiles("*" + fileExt);

            if (csvFiles.Length == 0)
            {
                Console.WriteLine("No CSV files found in the directory.");
                return;
            }

            // Find the latest file by creation date
            FileInfo latestCsvFile = csvFiles.OrderByDescending(f => f.CreationTime).First();

            string dateFormat    = DateTime.Now.ToString("dd/MM/yyyy");
            // string formattedDate = DateTime.Now.ToString("yyyyMMddHHmm");

            String csvFile  = latestCsvFile.Name; // Use the latest CSV file name
            // String fileName = csvFile + formattedDate + fileExt;

            String dataFile = Path.Combine(filePath, csvFile);

            List<string> returnedData = ReadDataFromCsv.GetDataFromCsv(dataFile, dateFormat);

            if (returnedData.Count > 0) FilterDataBeforeSendingToDB.PrepareAndCompareCsvAndDbData(returnedData); 

            DataArchiver.ArchiveQuery();
        }
    }
}