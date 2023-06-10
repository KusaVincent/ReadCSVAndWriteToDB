using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;

namespace ReadCSVAndWriteToDB
{
    internal class Logging 
    {
        public static void LogActivity (string message, List<string> logInfo)
        {
            DateTime now = DateTime.Now;

            String logDate  = now.ToString("yyyyMMdd");
            String logExt   = HandleConfigData.ConfigurationData("Authentication", "File:LogExt");
            String logFile  = HandleConfigData.ConfigurationData("Authentication", "File:LogName");
            String logDir   = HandleConfigData.ConfigurationData("Authentication", "Directory:LogDir");

            String logName  = logDir + logFile + logDate + logExt;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logName, LogEventLevel.Information,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            foreach (string logMsg in logInfo) {
                if (message == "Error")  {
                    Log.Error(logMsg);
                }  else if (message == "Information")  {
                    Log.Information(logMsg);
                }
            }

            Log.CloseAndFlush();
        }
    }
}