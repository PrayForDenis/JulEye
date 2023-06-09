using JulEye.Helpers;
using JulEye.HistoryLogic;
using JulEye.Interfaces;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JulEye
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        private IHistoryProvider _provider;

        public HistoryPage()
        {
            InitializeComponent();
            _provider = ServiceLocator.Take<IHistoryProvider>();
            MembersDataGrid.ItemsSource = _provider.Open();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _provider.Clear();
            NavigationService.GoBack();
        }
    }
}
