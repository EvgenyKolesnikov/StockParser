using StocksParser.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StocksParser.Model.DailyStock
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
        public double volume { get; set; }

        public virtual CompanyInfo CompanyInfo { get; set; }
    }
}
