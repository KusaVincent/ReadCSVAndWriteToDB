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

            String fileExt  = HandleConfigData.ConfigurationData("Authentication", "File:fileExt");
            String csvFile  = HandleConfigData.ConfigurationData("Authentication", "File:fileName");
            String filePath = HandleConfigData.ConfigurationData("Authentication", "Directory:fileDir");

            String fileName = csvFile + formattedDate + fileExt;

            String dataFile = filePath + fileName;

            List<string> returnedData = ReadDataFromCsv.GetDataFromCsv(dataFile, dateFormat);
            
            FilterDataBeforeSendingToDB.PrepareAndCompareCsvAndDbData(returnedData);
        }
    }
}