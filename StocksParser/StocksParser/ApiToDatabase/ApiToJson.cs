using StocksParser.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace StocksParser.ApiToDatabase
{
    //Получение данных с API и сохранение в JSON
    public static class ApiToJson 
    {
        private static string ApiToken = Settings.Default.ApiToken;
        const string RootPath = "Temp";
        public const string TempJsonPath = $"{RootPath}//tempjson.json";
        public enum OutputSize
        {
            compact,
            full
        }

        public static void GetDataFromAPI(string ticker, OutputSize outputSize)
        {
            if(!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
            }

            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={ticker}&outputsize={outputSize}&apikey={ApiToken}";
            Uri queryUri = new Uri(QUERY_URL);


            using (WebClient client = new WebClient())
            {
                dynamic json_data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                
                // -------------------------------------------------------------------------
                File.WriteAllText(TempJsonPath, client.DownloadString(queryUri));
                // do something with the json_data
            }
        }
    }
}
