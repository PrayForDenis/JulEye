using JulEye.Helpers;
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
    /// Логика взаимодействия для FpirDefinitionPage.xaml
    /// </summary>
    public partial class FpirDefinitionPage : Page
    {
        private ICriterionProvider _provider;

        public FpirDefinitionPage()
        {
            InitializeComponent();
            _provider = ServiceLocator.Take<ICriterionProvider>();

            if (!_provider.IsClear())
                _provider.Clear();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            string algorithmName = AlgorithmNameText.Text;
            string fpir = FpirText.Text;

            bool isCorrect = _provider.Save(algorithmName, fpir: fpir);

            if (isCorrect)
                NavigationService.Navigate(new Uri("LoadImagesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Digit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("CalculatePage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
