using JulEye.CriterionLogic;
using JulEye.Helpers;
using JulEye.HistoryLogic;
using JulEye.ImageLogic;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegisterServices();
            MainFrame.NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RegisterServices() 
        {
            Keras.Keras.DisablePySysConsoleLog = true;

            ServiceLocator.Register<IHistoryProvider>(new HistoryProvider());
            ServiceLocator.Register<ICriterionProvider>(new CriterionProvider());
            ServiceLocator.Register<IImageProvider>(new ImageProvider());
        }
    }
}
