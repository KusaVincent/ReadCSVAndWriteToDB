using System;
using System.IO;

namespace MpesaHoldingCSVToDB
{
    internal class ReadDataFromCsv 
    {
        public static List<string> GetDataFromCsv (String file, string dateFormat)
        {
            List<string> data = new List<string>();

            try 
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        string? line = reader.ReadLine();

                        if(line != null){
                            if (line.Contains(dateFormat))
                            {
                                data.Add(line); 
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                List<string> error = new List<string> { e.Message };
                Logging.LogActivity("Error", error);
            }
            
            return data;
        }
    }
}