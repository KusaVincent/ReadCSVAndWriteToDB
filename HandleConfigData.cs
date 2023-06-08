using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace MpesaHoldingCSVToDB
{
    internal class HandleConfigData
    {
        public static string ConfigurationData(string confData, string section)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                
            var configuration = configurationBuilder.Build();
            
            var authenticationValue = configuration[confData];
            var configurationData = configuration.GetSection(section).Value;
            
            return configurationData?.ToString() ?? "N/A";
        }
    }
}