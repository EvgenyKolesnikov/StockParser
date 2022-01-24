using StocksParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StocksParser.Views
{
    /// <summary>
    /// Логика взаимодействия для EditTicker.xaml
    /// </summary>
    public partial class EditTicker : Window
    {
        public EditTicker()
        {
            InitializeComponent();
            InitializeActions();

            DataContext = MainViewModel.GetInstance();
        }
      



        private void InitializeActions()
        {
            MainViewModel vm = MainViewModel.GetInstance();
            vm.CloseEditForm += () =>
            {
                this.Close();
            };
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
