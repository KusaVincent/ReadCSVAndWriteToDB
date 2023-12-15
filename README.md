# ReadCSVAndWriteToDB
Reading a CSV file and writing it to a Database (SQL Server{MSSQL})

---
**Table of contents**
1. Set up
2. understand the code
---
### Set up 
1. #### Prerequisite
    1. Ensure you have .NET installed
    1. run 
        ```c#
            dotnet restore
        ``` 
        to get all project dependancies installed.

2. #### appsettings.json
    Create a file named `appsettings.json`
    This file contain the config files and almost all static data.
    To import it, copy the below code and customize it to match your configs.

    ```json
    {
        "Authentication": "DatabaseConnectionString",
        "Database": {
            "ConnectionString": "Data Source=DB_IP;Initial Catalog=DB_NAME;User ID=DB_USER;Password=DB_PASSWORD",
            "SelectQuery": "SELECT Coulmn3 FROM [DB_NAME].[CSVTable] WHERE Coulmn3 = @value0",
            "InsertQuery": "INSERT INTO [DB_NAME].[CSVTable] (Coulmn1, Coulmn2, Coulmn3, Coulmn4, Coulmn5, Coulmn6) VALUES (@Value0,@Value1,@Value2,@Value3,@Value4,@Value5)"
        },
        "Directory" : {
            "FileDir" : "D:\\FILES\\",
            "LogDir"  : "D:\\FILES\\logs\\"
        },
        "File" : {
            "LogExt" : ".txt",
            "FileExt" : ".csv",
            "LogName" : "log_"
        }
    }
    ```
    file paths here a for windows environment, you can customize to match environment you are using.
---
### Understand the Code
1. #### Program.cs
    This is project entry point.
    It gets the csv file and date filter and pass it to class responsible for reading the file.
    The program is customized to get the file with the current timestamp.
    ```c#
        //example file
        MYFILENAME_2023061110823.csv
    ```
    This can be done away with by passing below variable as null in this file.
    ```c#
         string formattedDate = now.ToString("yyyyMMddHHmm");
    ```

1. #### ReadDataFromCsv.cs
    Gets the file, read the whole file and stores only the the row matching the filter in a list.
    The filter it is using is the one passed in 1. above.
    This class return a List back.

1. #### FilterDataBeforeSendingToDB.cs
    Takes the List returned 2. above, iterate through it, spliting the rows using the comma delimited and store it in a string array.
    This array is then checked in the database one by one, if it does not exist then it is added.

1. #### Database.cs
    Takes prepared data from 3. above and a statement.
    The statement `select` or `insert` tell the class what to do.
    Returns either db response or exception captured.

1. #### Logging.cs
    Responsible for logging all messages, both `ERR` and `INF` which is what is currently logged.
    The log file is created with date at the end
    ```c#
        //example file
        log_20230611.txt
    ```    
    The date can removed by setting below variable to null in this file.
    ```c#
        //example file
        String logDate  = now.ToString("yyyyMMdd");
    ```

1. #### HandleConfigData.cs
    It's responsibility is to read data from the config file `appsettings.json`
    Return NULL if it does not find the data being looked for.
---