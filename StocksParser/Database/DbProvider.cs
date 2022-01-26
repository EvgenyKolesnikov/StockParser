using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using StocksParser.Model.DailyStock;

namespace StocksParser.Database
{
    public class DbProvider
    {
        readonly AppDbContext dbProvider;
        public DbProvider()
        {
            dbProvider = new AppDbContext();
        }

        #region Create
        public void AddCompany(CompanyInfo companyInfo)
        {
            companyInfo.LastUserUpdate = DateTime.Now;
            dbProvider.Add(companyInfo);
            dbProvider.SaveChanges();
        }
        #endregion


        #region Read
        //получение всех объектов CompanyInfo
        public List<CompanyInfo> GetAllCompanyInfos()
        {
            return dbProvider.CompanyInfos.Include(i => i.DailyStocks).ToList();
        }

        //получение одного объекта СompanyInfo
        public CompanyInfo GetCompanyByCompanyInfo(CompanyInfo companyInfo)
        {
            var item = dbProvider.CompanyInfos.Include(i => i.DailyStocks).FirstOrDefault(i => i.ticker == companyInfo.ticker);
            return item;
        }

        public CompanyInfo GetCompanyByTickerName(string tickerName)
        {
            var item = dbProvider.CompanyInfos.Include(i => i.DailyStocks).FirstOrDefault(i => i.ticker == tickerName);
            return item;
        }
        #endregion


        #region Update
        //Обновить имя компании, так как изначально оно не тянется с Апи, нужно заносить руками.
        public void UpdateCompanyInfoName(CompanyInfo company)
        {
            CompanyInfo? CompanyInfo = dbProvider.CompanyInfos.Where(i => i.ticker == company.ticker).FirstOrDefault();

            if (CompanyInfo != null)
            {
                CompanyInfo.Name = company.Name;
                dbProvider.SaveChanges();
            } 
        }
        

        //Обновление тикера
        public int UpdateCompany(CompanyInfo ApiItem)
        {
            int counter = 0;

            if (ApiItem != null)
            {
                CompanyInfo? DatabaseItem = dbProvider.CompanyInfos.Where(i => i.ticker == ApiItem.ticker).Include(i => i.DailyStocks).FirstOrDefault();
                DateTime LastDateUpdate = DatabaseItem.DailyStocks.Select(i => i.dateTime).OrderByDescending(i => i).FirstOrDefault();

                foreach (var DailyStock in ApiItem.DailyStocks)
                {
                    if (DailyStock.dateTime > LastDateUpdate)
                    {
                        DailyStock.CompanyInfoid = DatabaseItem.id;

                        dbProvider.DailyStocks.Add(DailyStock);
                        dbProvider.SaveChanges();
                        counter++;
                    }
                }
                DatabaseItem.LastUserUpdate = DateTime.Now; 
                DatabaseItem.LastRefreshed = ApiItem.LastRefreshed; 
                dbProvider.SaveChanges();
            }
            return counter;
        }
        #endregion


        #region Delete
        //Удаление CompanyInfo и связанных записей в DailyStocks
        public void DeleteCompany(CompanyInfo company)
        {
            //в бд должно быть настроено каскадное удаление
            var itemToRemove = dbProvider.CompanyInfos.Where(i => i.id == company.id).First();
            dbProvider.CompanyInfos.Remove(itemToRemove);
            dbProvider.SaveChanges();
        }
        #endregion
    }
}
