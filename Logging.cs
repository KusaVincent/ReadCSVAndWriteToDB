using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;

namespace MpesaHoldingCSVToDB
{
    internal class Logging 
    {
        public static void LogActivity (string message, List<string> logInfo)
        {
            DateTime now = DateTime.Now;

            String logDate = now.ToString("yyyyMMdd");
            String logFile = HandleConfigData.ConfigurationData("Authentication", "File:logName");
            String logDir = HandleConfigData.ConfigurationData("Authentication", "Directory:logDir");

            String logName = logDir + logFile + logDate + ".txt";

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
                .WriteTo.File(logName, LogEventLevel.Information,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            foreach (string logs in logInfo) {
                if (message == "Error")  {
                    Log.Error(logs);
                }  else if (message == "Information")  {
                    Log.Information(logs);
                }
            }

            Log.CloseAndFlush();
        }
    }
}