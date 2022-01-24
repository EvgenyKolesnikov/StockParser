using StocksParser.ApiToDatabase;
using StocksParser.Database;
using StocksParser.Properties;
using StocksParser.ViewModel.Commands;
using StocksParser.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using StocksParser.Model.DailyStock;
using System.Collections.ObjectModel;
using System.Threading;

namespace StocksParser.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public static Action RefreshCollectionsAction { get; set; }
        public Action StopAnimation { get; set; }
        public Action StartAnimation { get; set; }
        public Action CloseEditForm { get; set; }


        public DbProvider dbProvider;
        public string SelectedCompanyName { get; set; }                     //Add Form Имя компании
        public string SelectedTickerName { get; set; }                     //Add Form Имя тикера
        public static CompanyInfo SelectedCompanyInfo { get; set; }



        public TriggerCommand AddTickerCommand { get; set; }               // Добавить Тикер
        public TriggerCommand<object> RefreshTickerCommand { get; set; }   // Обновить Тикер
        public TriggerCommand<object> DeleteTickerCommand { get; set; }    // Удалить Тикер

        public TriggerCommand OpenAddFormCommand { get; set; }             // Открыть Add Ticker Form
        public TriggerCommand<object> OpenEditFormCommand { get; set; }    // Открыть Edit Ticker Form

        public TriggerCommand EditButtonSubmit { get; set; }               // Подтвердить изменения в Edit Form 
        public TriggerCommand SubmitApiTokenCommand { get; set; }          // Подтвердить изменения ApiToken
        public TriggerCommand SubmitConnectionStringCommand { get; set; }  // Подтвердить изменения Connection String
        public TriggerCommand CreateDatabaseCommand { get; set; }          // Создать базу данных



        public static MainViewModel _instance;
        public static MainViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainViewModel();
            }
            return _instance;
        }

        public MainViewModel()
        {
            //   var a = AppSettings.Temp_filePath;
            dbProvider = new DbProvider();
            CheckDatabaseConnection();
            InitializeData();
            InitializeCommands();
            InitializeActions();
        }

        private void InitializeActions()
        {
            RefreshCollectionsAction += () =>
            {
                InitializeData();
            };
        }

        private void InitializeCommands()
        {
            AddTickerCommand = new TriggerCommand(HandleAddTicker);                           // Добавить Тикер
            RefreshTickerCommand = new TriggerCommand<object>(HandleRefreshTicker);           // Обновить Тикер
            DeleteTickerCommand = new TriggerCommand<object>(HandleDelete);                   // Удалить Тикер
            OpenAddFormCommand = new TriggerCommand(HandleOpenAddForm);                       // Открыть Add Ticker Form
            OpenEditFormCommand = new TriggerCommand<object>(HandleOpenEditForm);             // Открыть Edit Ticker Form
            EditButtonSubmit = new TriggerCommand(HandleEditTicker);                          // Подтвердить изменения в Edit Form
            SubmitApiTokenCommand = new TriggerCommand(HandleSubmitApiToken);                 // Подтвердить изменения ApiToken
            SubmitConnectionStringCommand = new TriggerCommand(HandleSubmitConnectionString); // Подтвердить изменения Connection String
            CreateDatabaseCommand = new TriggerCommand(HandleCreateDatabase);                 // Создать базу данных
        }


        //Проверка соединения с базой данных
        private void CheckDatabaseConnection()
        {
            try
            {
                using (AppDbContext appDbContext = new AppDbContext())
                {
                    if (!appDbContext.Database.CanConnect())
                    {
                        MessageBox.Show("Подключение к базе данных отсутствует");

                        isConnectionDatabase = false;
                        CompaniesInfo = null;
                        return;
                    }
                }
                isConnectionDatabase = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Инициализация данных
        private void InitializeData()
        {
            ApiToken = Settings.Default.ApiToken;
            ConnectionString = Settings.Default.Connection;
            CompaniesInfo = dbProvider.GetAllCompanyInfos();
            TickerIsRefreshingNow = false;
        }



        private async Task<bool> AddTicker()
        {
            try
            {
                await Task.Run(() =>
                {
                    ApiToJson.GetDataFromAPI(SelectedTickerName, ApiToJson.OutputSize.full);
                });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Кнопка Sumbit в форме Add 
        private async void HandleAddTicker()
        {

            if (dbProvider.GetCompanyByTickerName(SelectedTickerName) != null)
            {
                MessageBox.Show($"ticker: " + SelectedTickerName + " has already existed");
                return;
            }

            StartAnimation?.Invoke();

            int timeout = 40000;
            var task = AddTicker();

            //отмена задачи по истечению timeout
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                await task;
            }
            else
            {
                // timeout/cancellation logic

                MessageBox.Show("Request Timeout Error");
            }

            try
            {
                if (task.IsCompleted && task.Result)
                {
                    var ApiObject = JsonToObject.ParseDailyStock();
                    var DatabaseObject = ObjectToDatabaseObject.ParseDailyStock(ApiObject);
                    DatabaseObject.Name = SelectedCompanyName;
                    dbProvider.AddCompany(DatabaseObject);


                    MessageBox.Show(SelectedTickerName + " was added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            RefreshCollectionsAction?.Invoke();
            StopAnimation?.Invoke();
        }

        private async Task<bool> RefreshTicker()
        {
            try
            {
                await Task.Run(() =>
                {
                    if (SelectedCompanyInfo.LastRefreshed < DateTime.Now)
                    {
                        //full - выгрузка тикера за 20 лет
                        //compact - выгрузка за последние ~100 дней
                        var outputSize = (DateTime.Now - SelectedCompanyInfo.LastRefreshed).Days > 90 ? ApiToJson.OutputSize.full : ApiToJson.OutputSize.compact;
                        ApiToJson.GetDataFromAPI(SelectedCompanyInfo.ticker, outputSize);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
        }



        //Кнопка Refresh
        private async void HandleRefreshTicker(object CompanyInfo)
        {
            var Datacontext = ((Button)CompanyInfo).DataContext;
            if (Datacontext is CompanyInfo companyInfo)
            {
                SelectedCompanyInfo = companyInfo;
            }

            int timeout = 40000;
            var task = RefreshTicker();
            TickerIsRefreshingNow = true;


            try
            {
                //отмена задачи по истечению timeout
                if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                {
                    await task;
                }
                else
                {
                    // timeout/cancellation logic
                    TickerIsRefreshingNow = false;
                    MessageBox.Show("Request Timeout Error");
                }
                if (task.IsCompleted && task.Result)
                {
                    var ApiObject = JsonToObject.ParseDailyStock();
                    var DatabaseObject = ObjectToDatabaseObject.ParseDailyStock(ApiObject);
                    int countRefresh = dbProvider.UpdateCompany(DatabaseObject);

                    TickerIsRefreshingNow = false;
                    RefreshCollectionsAction?.Invoke();
                    MessageBox.Show($"Тикер: {DatabaseObject.ticker} \n " +
                                           $"Добавлено записей: {countRefresh}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Кнопка Delete
        private async void HandleDelete(object CompanyInfo)
        {
            var Datacontext = ((Button)CompanyInfo).DataContext;
            if (Datacontext is CompanyInfo companyInfo)
            {
                SelectedCompanyInfo = companyInfo;
            }

            await Task.Run(() =>
            {
                TickerIsRefreshingNow = true;

                dbProvider.DeleteCompany(SelectedCompanyInfo);
                RefreshCollectionsAction?.Invoke();
                MessageBox.Show(SelectedCompanyInfo.Name + " was deleted");

                TickerIsRefreshingNow = false;
            });
        }


        //создание базы данных, при ее отсутсвии
        private void HandleCreateDatabase()
        {
            using (AppDbContext appDbContext = new AppDbContext())
            {
                if (!appDbContext.Database.CanConnect())
                {
                    MessageBoxResult result = MessageBox.Show("Create Database?", "Database", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            appDbContext.Database.EnsureCreated();
                            MessageBox.Show("База данных создана");

                            InitializeData();
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
            }
        }

        //Кнопка submit ConnectionString
        private void HandleSubmitConnectionString()
        {
            Settings.Default.Connection = ConnectionString;
            Settings.Default.Save();

            CheckDatabaseConnection();
            InitializeData();
        }

        //Кнопка submit ApiToken
        private void HandleSubmitApiToken()
        {
            Settings.Default.ApiToken = ApiToken;
            Settings.Default.Save();
        }


        //Открыть форму Add ticker
        private void HandleOpenAddForm()
        {
            var win = new AddTicker();
            win.Show();
        }

        //Открыть форму Edit ticker
        private void HandleOpenEditForm(object CompanyInfo)
        {
            var Datacontext = ((Button)CompanyInfo).DataContext;

            if (Datacontext is CompanyInfo companyInfo)
            {
                SelectedCompanyInfo = companyInfo;
            }

            var win = new EditTicker();
            win.Show();
        }

        //Кнопка submit в Edit Form
        public void HandleEditTicker()
        {
            dbProvider.UpdateCompanyInfoName(SelectedCompanyInfo);
            CloseEditForm?.Invoke();
        }
    }
}
