using JulEye.Helpers;
using JulEye.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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
    /// Логика взаимодействия для GenerateVectorPage.xaml
    /// </summary>
    public partial class GenerateVectorPage : Page
    {
        private IImageProvider _provider;

        public GenerateVectorPage()
        {
            InitializeComponent();
            _provider = ServiceLocator.Take<IImageProvider>();

            if (_provider.IsNeedToGenerateVectors())
                DisableButtons();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("ChooseMetricPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("LoadImagesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void GenerateVectorsButton_Click(object sender, RoutedEventArgs e)
        {
            if (Generate())
                ContinueButton.IsEnabled = true;
        }

        private bool Generate() 
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Models files (*.h5) | *.h5";
            openFileDialog.Title = "Select Model";

            if (openFileDialog.ShowDialog() == true)
            {
                var file = openFileDialog.FileNames.FirstOrDefault();

                if (!_provider.GenerateVectors(file))
                    return false;

                return true;
            }
            return false;
        }

        private void DisableButtons()
        {
            ContinueButton.IsEnabled = false;
        }
    }
}
