using System;

namespace ReadCSVAndWriteToDB
{
    internal class Program 
    {
        static void Main (string[] args)
        {
            DateTime now = DateTime.Now;
            
            string dateFormat    = now.ToString("dd/MM/yyyy");
            string formattedDate = now.ToString("yyyyMMddHHmm");

            String fileExt  = HandleConfigData.ConfigurationData("Authentication", "File:FileExt");
            String csvFile  = HandleConfigData.ConfigurationData("Authentication", "File:FileName");
            String filePath = HandleConfigData.ConfigurationData("Authentication", "Directory:FileDir");

            String fileName = csvFile + formattedDate + fileExt;

            String dataFile = filePath + fileName;

            List<string> returnedData = ReadDataFromCsv.GetDataFromCsv(dataFile, dateFormat);

            if (returnedData.Count > 0) FilterDataBeforeSendingToDB.PrepareAndCompareCsvAndDbData(returnedData); 
        }
    }
}