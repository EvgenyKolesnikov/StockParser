using Microsoft.EntityFrameworkCore;
using StocksParser.Model;
using StocksParser.Properties;
using System;
using System.Collections.Generic;
using StocksParser.Model.DailyStock;

namespace StocksParser.Database
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext() 
        { 
            // Database.EnsureDeleted();
            // Database.EnsureCreated();
        }

        public DbSet<DailyStocks> DailyStocks { get; set; } //информация по тикерам
        public DbSet<CompanyInfo> CompanyInfos { get; set; } //информация о компании

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.Default.Connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyInfo>().HasMany(i => i.DailyStocks).WithOne(i => i.CompanyInfo).IsRequired();
        }
    }
}
