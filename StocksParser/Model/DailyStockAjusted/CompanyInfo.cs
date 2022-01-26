using StocksParser.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StocksParser.Model.DailyStockAjusted
{
    public class CompanyInfo : BaseEntity
    {
        public string ticker { get; set; }
        public string? Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime LastRefreshed { get; set; }
        public DateTime LastUserUpdate { get; set; } 
        public List<DailyStocks> DailyStocks { get; set; } = new List<DailyStocks>();


        [NotMapped]
        public int RecordsCount        //количество записей DailyStocks
        {
            get => DailyStocks.Count;
        }
    }
}
