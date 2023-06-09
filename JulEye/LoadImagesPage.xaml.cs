using JulEye.Helpers;
using JulEye.Interfaces;
using Microsoft.Win32;
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
    /// Логика взаимодействия для LoadImagesPage.xaml
    /// </summary>
    public partial class LoadImagesPage : Page
    {
        private IImageProvider _provider;

        public LoadImagesPage()
        {
            InitializeComponent();
            _provider = ServiceLocator.Take<IImageProvider>();

            if (_provider.IsClear())
                DisableButtons();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("GenerateVectorPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _provider.Clear();
            NavigationService.Navigate(new Uri("CalculatePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void LoadJsonInBaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoadFromJson(true))
                LoadJsonInTestButton.IsEnabled = true;
        }

        private void LoadJsonInTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoadFromJson())
                ContinueButton.IsEnabled = true;
        }

        private void LoadImagesInBaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoadImages(true))
                LoadTestImagesButton.IsEnabled = true;
        }

        private void LoadTestImagesButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoadImages())
                LoadJsonInBaseButton.IsEnabled = true;
        }

        private bool LoadImages(bool inBase = false)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.Title = "Select Images";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    if (!_provider.Save(file, inBase))
                        return false;
                }
                return true;
            }
            return false;
        }

        private bool LoadFromJson(bool inBase = false)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JSON files (*.json) | *.json";
            openFileDialog.Title = "Select JSONs";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    if (!_provider.Save(file, inBase, true))
                        return false;
                }
                return true;
            }
            return false;
        }

        private void DisableButtons()
        {
            LoadJsonInBaseButton.IsEnabled = false;
            LoadJsonInTestButton.IsEnabled = false;
            LoadTestImagesButton.IsEnabled = false;
            ContinueButton.IsEnabled = false;
        }
    }
}
