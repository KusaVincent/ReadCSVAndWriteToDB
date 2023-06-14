using System;
using System.Collections.Generic;

namespace ReadCSVAndWriteToDB
{
    internal class FilterDataBeforeSendingToDB
    {
        public static void PrepareAndCompareCsvAndDbData(List<string> csvData)
        {
            List<string> databaseResponse = new List<string>();
            Dictionary<string, string> dbValue = new Dictionary<string, string>();
            
            foreach (string csvLine in csvData)
            {
                string[] csvValues = csvLine.Split(",");
                
                if (csvValues.Length > 2)
                {
                    dbValue["value2"] = csvValues[2];
                    List<string> checkAvailability = DataBase.ExecuteQuery("select", dbValue);

                    if(checkAvailability.Count == 0) {
                        dbValue["value0"] = csvValues[0];
                        dbValue["value1"] = csvValues[1];
                        dbValue["value3"] = csvValues[3];
                        dbValue["value4"] = csvValues[4];
                        dbValue["value5"] = csvValues[5];
                        databaseResponse = DataBase.ExecuteQuery("insert",dbValue);
                    }
                }
            }
            
            Logging.LogActivity("Information", databaseResponse);
        }
    }
}