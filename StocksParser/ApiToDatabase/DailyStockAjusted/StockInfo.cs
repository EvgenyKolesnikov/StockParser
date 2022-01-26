using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StocksParser.ApiToDatabase.DailyStockAjusted
{
    //модель парсинга JSON в объект

    [NotMapped]
    public class StockInfo
    {
        [JsonProperty("Meta Data")]
        public MetaData metadata { get; set; }

        [JsonProperty("Time Series (Daily)")]
        public Dictionary<DateTime, DailyStock> DailyTimeSeries { get; set; }
    }
}
