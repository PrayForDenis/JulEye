using JulEye.CalculationLogic;
using JulEye.Helpers;
using JulEye.HistoryLogic;
using JulEye.Interfaces;
using JulEye.MetricLogic;
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
    /// Логика взаимодействия для ChooseMetricPage.xaml
    /// </summary>
    public partial class ChooseMetricPage : Page
    {
        private IMetric _metric;
        private Calculation _calculation;

        public ChooseMetricPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceLocator.TryTake<IMetric>(out _metric))
                ServiceLocator.UnRegister<IMetric>();
            NavigationService.Navigate(new Uri("GenerateVectorPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem metricValueItem = (ComboBoxItem) MetricsChooser.SelectedValue;
            var metricValue = metricValueItem.Content.ToString();
           
            if (!ServiceLocator.TryTake<IMetric>(out _metric))
                CreateInstance(metricValue);

            if (_calculation == null)
                _calculation = new Calculation();
            
            _calculation.Calculate();
        }

        private void CreateInstance(string metricValue) 
        {
            if (metricValue.Equals("Евклидово расстояние"))
                ServiceLocator.Register<IMetric>(new EuclideanDistance());
            else if (metricValue.Equals("Косинусное расстояние"))
                ServiceLocator.Register<IMetric>(new CosineDistance());
            else
                ServiceLocator.Register<IMetric>(new MahalanobisDistance());
        }
    }
}
