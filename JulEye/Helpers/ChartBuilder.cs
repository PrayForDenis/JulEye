using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace JulEye.Helpers
{
    internal static class ChartBuilder
    {
        public static void BuildGraph(float[] xValues, float[] yValues,
                                    string windowTitle, string chartTitle,
                                    string xAxisTitle, string yAxisTitle)
        {
            try
            {
                var plotModel = new PlotModel();
                plotModel.Title = chartTitle;

                var lineSeries = new LineSeries();

                for (int i = 0; i < xValues.Length; i++)
                {
                    lineSeries.Points.Add(new DataPoint(xValues[i], yValues[i]));
                }

                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xAxisTitle });
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yAxisTitle });

                plotModel.Series.Add(lineSeries);

                var window = new Window();
                window.Title = windowTitle;
                window.Width = 600;
                window.Height = 600;

                var plotView = new OxyPlot.Wpf.PlotView();
                plotView.Model = plotModel;

                window.Content = plotView;
                window.Show();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Произошла ошибка во время построения графика!\n" + e.Message);
            }
        }
    }
}
