using System;
using System.Collections.Generic;

namespace ReadCSVAndWriteToDB
{
    internal class FilterDataBeforeSendingToDB
    {
        public static void PrepareAndCompareCsvAndDbData(List<string> csvData)
        {
            List<string> databaseResponse = new List<string>();

            foreach (string csvLine in csvData)
            {
                string[] csvValues = csvLine.Split(",");
                
                if (csvValues.Length > 2)
                {
                    List<string> checkAvailability = DataBase.ExecuteQuery("select", csvValues[2]);

                    if(checkAvailability.Count == 0) {
                        databaseResponse = DataBase.ExecuteQuery("insert", csvValues[0], csvValues[1], csvValues[2], csvValues[3], csvValues[4], csvValues[5]);
                    }
                }
            }

            Logging.LogActivity("Information", databaseResponse);
        }
    }
}