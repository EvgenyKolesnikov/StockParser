using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using StocksParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace StocksParser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly CompositeDisposable _disposable;
        public App()
        {
            _disposable = new CompositeDisposable();

       

        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BootStrapper.Start();
            var window = new MainWindow();


            window.DataContext = BootStrapper.RootVisual;

            window.Closed += (s, a) =>
            {
                _disposable.Dispose();
                BootStrapper.Stop();
            };

            window.Show();

        }  
    }
}

