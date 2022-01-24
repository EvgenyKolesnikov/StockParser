
namespace StocksParser.ApiToDatabase
{
    //Конвертация Объекта приходящего из JSON в Объект для Базы Данных
    public static class ObjectToDatabaseObject
    {

        //Конвертация DailyStockAjusted
        public static Model.DailyStockAjusted.CompanyInfo ParseDailyStockAjusted(DailyStockAjusted.StockInfo stockInfo)
        {
            Model.DailyStockAjusted.CompanyInfo companyInfo = new Model.DailyStockAjusted.CompanyInfo();

            companyInfo.ticker = stockInfo.metadata.Symbol;
            companyInfo.LastRefreshed = stockInfo.metadata.LastRefreshed;

            foreach (var stock in stockInfo.DailyTimeSeries)
            {
                Model.DailyStockAjusted.DailyStocks dailyStock = new Model.DailyStockAjusted.DailyStocks()
                {
                    ticker = stockInfo.metadata.Symbol,
                    dateTime = stock.Key,
                    open = stock.Value.open,
                    high = stock.Value.high,
                    low = stock.Value.low,
                    close = stock.Value.close,
                    adjusted_close = stock.Value.adjusted_close,
                    volume = stock.Value.volume,
                    dividend_amount = stock.Value.dividend_volume,
                    split_coefficient = stock.Value.split_coefficient
                };
                companyInfo.DailyStocks.Add(dailyStock);
            }
            return companyInfo;
        }

        //Конвертация DailyStock
        public static Model.DailyStock.CompanyInfo ParseDailyStock(DailyStock.StockInfo stockInfo)
        {
            Model.DailyStock.CompanyInfo companyInfo = new Model.DailyStock.CompanyInfo();

            companyInfo.ticker = stockInfo.metadata.Symbol;
            companyInfo.LastRefreshed = stockInfo.metadata.LastRefreshed;

            foreach (var stock in stockInfo.DailyTimeSeries)
            {
                Model.DailyStock.DailyStocks dailyStock = new Model.DailyStock.DailyStocks()
                {
                    ticker = stockInfo.metadata.Symbol,
                    dateTime = stock.Key,
                    open = stock.Value.open,
                    high = stock.Value.high,
                    low = stock.Value.low,
                    close = stock.Value.close,
                    volume = stock.Value.volume,

                };
                companyInfo.DailyStocks.Add(dailyStock);

            }
            return companyInfo;
        }
    }
}
