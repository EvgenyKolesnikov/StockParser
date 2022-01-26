using System.ComponentModel.DataAnnotations.Schema;
using StocksParser.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace StocksParser.Model.DailyStockAjusted
{
    public class DailyStocks : BaseEntity
    {
        [Required]
        [ForeignKey("CompanyInfoid")]
        public int CompanyInfoid { get; set; }


        public string ticker { get; set; }
        [Column(TypeName = "date")]
        public DateTime dateTime { get; set; }
        public double open { get; set; }

        public double high { get; set; }

        public double low { get; set; }

        public double close { get; set; }
        public double adjusted_close { get; set; }

        public double volume { get; set; }
        public double dividend_amount { get; set; }
        public double split_coefficient { get; set; }

        public virtual CompanyInfo CompanyInfo { get; set; } 
    }
}