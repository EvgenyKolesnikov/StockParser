using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksParser.ApiToDatabase.DailyStockAjusted
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

        [JsonProperty("5. adjusted close")]
        public double adjusted_close { get; set; }

        [JsonProperty("6. volume")]
        public double volume { get; set; }

        [JsonProperty("7. dividend amount")]
        public double dividend_volume { get; set; }

        [JsonProperty("8. split coefficient")]
        public double split_coefficient { get; set; }

        public int meta { get; set; }
        public DateTime date { get; set; }


    }
}
