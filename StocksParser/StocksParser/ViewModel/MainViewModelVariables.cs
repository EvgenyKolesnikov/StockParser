using StocksParser.Model.DailyStock;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StocksParser.ViewModel
{
    partial class MainViewModel
    {
        
        private  List<CompanyInfo> companyInfos;
        public  List<CompanyInfo> CompaniesInfo
        {
            get => companyInfos;
            set
            {
                companyInfos = value;
                RaisePropertyChanged(nameof(CompaniesInfo));
            }
        }

        private bool _TickerIsRefreshingNow;
        public bool TickerIsRefreshingNow
        {
            get => _TickerIsRefreshingNow;
            set
            {
               _TickerIsRefreshingNow = value;
                RaisePropertyChanged(nameof(TickerIsRefreshingNow));
            }
        }




        private string _apiToken;
        public string ApiToken
        {
            get => _apiToken;
            set
            {
                _apiToken = value;
                RaisePropertyChanged(nameof(ApiToken));
            }
        }


        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                RaisePropertyChanged(nameof(ConnectionString));
            }
        }


        private static string _companyName; // ?????
        public static string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
            }
        }



        private bool _isConnectionDatabase;
        public bool isConnectionDatabase
        {
            get => _isConnectionDatabase;
            set
            {
                _isConnectionDatabase = value;
                RaisePropertyChanged(nameof(isConnectionDatabase));
            }
        }
    }
}
