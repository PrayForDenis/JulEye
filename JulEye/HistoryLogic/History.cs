using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace JulEye.HistoryLogic
{
    internal class History : IHistoryViewer
    {
        private List<Result> _results = new List<Result>();
        private readonly IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public IReadOnlyList<Result> Results => _results;

        public IReadOnlyList<Result> LoadFromFile(string fileName)
        {
            var file = fileName;
            try
            {
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    var splitLine = line.Split(';');
                    var Result = new Result
                    (
                        splitLine[0],
                        splitLine[1],
                        double.Parse(splitLine[2], formatter),
                        double.Parse(splitLine[3], formatter),
                        double.Parse(splitLine[4], formatter),
                        double.Parse(splitLine[5], formatter),
                        double.Parse(splitLine[6], formatter),
                        double.Parse(splitLine[7], formatter),
                        splitLine[8]
                    );
                    _results.Add(Result);
                }
                return Results;
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка при загрузке истории работы!\n" + e.Message);
                return null;
            }
        }

        public void Save(string algorithmName, string metric, double fpir, double fnir, double tpir,
                        double detectionTime, double generateVectorTime, double searchTime, double computingCost, 
                        string fileName)
        {
            var file = fileName;
            try
            {
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    writer.WriteLine(algorithmName + ";" + metric + ";" + Format(fpir) + ";" +
                        Format(fnir) + ";" + Format(tpir) + ";" +
                        Format(detectionTime) + ";" + Format(generateVectorTime) + ";" +
                        Format(searchTime) + ";" + Format(computingCost) + "%");
                }
                MessageBox.Show("Запись в историю успешно завершена!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка во время сохранения истории работы!\n" + e.Message);
                return;
            }
        }

        public void Clear() 
        {
            _results.Clear();
        }

        private string Format(double value)
        {
            return string.Format("{0:0.###}", value).Replace(',', '.');
        }
    }
}
