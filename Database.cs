using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ReadCSVAndWriteToDB
{
    internal class DataBase
    {
        public static List<string> ExecuteQuery(string statement, Dictionary<string, string> value)
        {
            List<string> result  = new List<string>();
            
            var connectionString = HandleConfigData.ConfigurationData("Authentication", "Database:ConnectionString");

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    if (statement == "select") {
                        string query = HandleConfigData.ConfigurationData("Authentication", "Database:SelectQuery");
                        
                        using (SqlCommand command = new SqlCommand(query, sqlConnection))
                        {
                            command.Parameters.AddWithValue("@Value0", value["value2"]);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnValue = reader.GetValue(i)?.ToString() ?? "NULL";
                                        result.Add(columnValue);
                                    }
                                }
                            }
                        }
                    }  else if (statement == "insert") {
                        string query = HandleConfigData.ConfigurationData("Authentication", "Database:InsertQuery");
                        
                        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                        {
                            sqlCommand.Parameters.AddWithValue("@Value0", value["value0"]);
                            sqlCommand.Parameters.AddWithValue("@Value1", value["value1"]);
                            sqlCommand.Parameters.AddWithValue("@Value2", value["value2"]);
                            sqlCommand.Parameters.AddWithValue("@Value3", value["value3"]);
                            sqlCommand.Parameters.AddWithValue("@Value4", value["value4"]);
                            sqlCommand.Parameters.AddWithValue("@Value5", value["value5"]);

                            int rowsAffected = sqlCommand.ExecuteNonQuery();
                            result.Add(rowsAffected.ToString());
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                List<string> error = new List<string> { e.Message };
                Logging.LogActivity("Error", error);
            }
            
            return result;
        }
    }
}