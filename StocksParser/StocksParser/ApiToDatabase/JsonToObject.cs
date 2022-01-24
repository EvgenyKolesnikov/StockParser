using Newtonsoft.Json;
using System.IO;
using DSA = StocksParser.ApiToDatabase.DailyStockAjusted; //модель для DailyStockAjusted
using DS = StocksParser.ApiToDatabase.DailyStock;   //модель для DailyStock
using System;


namespace StocksParser.ApiToDatabase
{
    //Парсинг JSON приходящего с API в Объект
    public static class JsonToObject
    {
        public static DSA.StockInfo ParseDailyStockAjusted()
        {
            var jsonText = File.ReadAllText(ApiToJson.TempJsonPath);
            DSA.StockInfo stocks = JsonConvert.DeserializeObject<DSA.StockInfo>(jsonText);

            if (stocks.metadata == null)
                throw new Exception(jsonText);

            return stocks;
        }

        public static DS.StockInfo ParseDailyStock()
        {
            var jsonText = File.ReadAllText(ApiToJson.TempJsonPath);
            DS.StockInfo stocks = JsonConvert.DeserializeObject<DS.StockInfo>(jsonText);


            if (stocks.metadata == null)
                throw new Exception(jsonText);
           
            return stocks;
        }
    }
}
