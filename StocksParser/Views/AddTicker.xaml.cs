using StocksParser.ApiToDatabase;
using StocksParser.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StocksParser.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTicker.xaml
    /// </summary>
    public partial class AddTicker : Window
    {
        public AddTicker()
        {
            InitializeComponent();
            InitializeActions();
            DataContext = MainViewModel.GetInstance();
        }

       

        private void InitializeActions()
        {
            MainViewModel vm = MainViewModel.GetInstance();

            //Анимация loader
            vm.StopAnimation += () =>
            {
                rotate.BeginAnimation(RotateTransform.AngleProperty, null);
                loader.Visibility = Visibility.Collapsed;
            };

            //Анимация loader
            vm.StartAnimation += () =>
            {
                loader.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation
                {
                    By = 360,
                    Duration = new Duration(TimeSpan.FromSeconds(1)),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                rotate.BeginAnimation(RotateTransform.AngleProperty, animation);
            };
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
