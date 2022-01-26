using Newtonsoft.Json;
using System;

namespace StocksParser.ApiToDatabase.DailyStock
{
    public class DailyStock
    {
        [JsonProperty("1. open")]
        public double open { get; set; }

        [JsonProperty("2. high")]
        public double high { get; set; }

        [JsonProperty("3. low")]
        public double low { get; set; }

        [JsonProperty("4. close")]
        public double close { get; set; }

        [JsonProperty("5. volume")]
        public double volume { get; set; }

        public int meta { get; set; }
        public DateTime date { get; set; }
    }
}
