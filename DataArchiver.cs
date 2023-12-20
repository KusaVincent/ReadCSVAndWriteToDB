using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ReadCSVAndWriteToDB
{
    internal class DataArchiver
    {
        public static void ArchiveQuery()
        {
            DateTime now = DateTime.Now;

            String archivePeriod    = HandleConfigData.ConfigurationData("Authentication", "Archive:CleanUpPeriod");
            var connectionString    = HandleConfigData.ConfigurationData("Authentication", "Database:ConnectionString");

            DateTime archivePeriodInWeeks = now.AddDays(int.Parse(archivePeriod) * -7);

            string formattedArchivePeriodInWeeks = archivePeriodInWeeks.ToString("yyyy-MM-dd HH:mm:ss.fff");
            
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    List<string> result     = new List<string>();

                    string query = HandleConfigData.ConfigurationData("Authentication", "Database:DeleteQuery");
                    
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Value0", formattedArchivePeriodInWeeks);

                        int archiverOutcome = command.ExecuteNonQuery();
                        result.Add(archiverOutcome.ToString());
                    }

                    sqlConnection.Close();

                    Logging.LogActivity("Information", result);
                }
            }
            catch (Exception e)
            {
                List<string> error = new List<string> { e.Message };
                Logging.LogActivity("Error", error);
            }
        }
    }
}