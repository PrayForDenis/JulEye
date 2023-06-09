using JulEye.CriterionLogic;
using JulEye.Helpers;
using JulEye.HistoryLogic;
using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для CalculatePage.xaml
    /// </summary>
    public partial class CalculatePage : Page
    {
        public CalculatePage()
        {
            InitializeComponent();
        }

        private void CalculateWithOptimumButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("FpirDefinitionPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void DefineCriterionManuallyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("ThresholdDefinitionPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
