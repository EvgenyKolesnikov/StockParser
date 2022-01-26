using StocksParser.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace StocksParser.Model.DailyStock
{
    public class CompanyInfo : BaseEntity
    {
        public string ticker { get; set; } // Тикер
        public string? Name { get; set; } // Имя компании

        [Column(TypeName = "date")]
        public DateTime LastRefreshed { get; set; } // Дата обновления данных из апишки
        public DateTime LastUserUpdate { get; set; } // Дата обновления юзером
        public List<DailyStocks> DailyStocks { get; set; } = new List<DailyStocks>();



        [NotMapped]
        public int RecordsCount        //количество записей DailyStocks
        {
            get => DailyStocks.Count;
        }
    }
}
