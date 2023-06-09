using JulEye.CriterionLogic;
using JulEye.Helpers;
using JulEye.HistoryLogic;
using JulEye.ImageLogic;
using JulEye.Interfaces;
using MathNet.Numerics.Statistics;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JulEye.CalculationLogic
{
    internal class Calculation
    {
        private CriterionProvider _criterion;
        private ImageProvider _image;
        private HistoryProvider _history;
        private IMetric _metric;

        private List<float> _tpir = new List<float>();
        private List<float> _fpir = new List<float>();
        private List<float> _fnir = new List<float>();

        private List<float> _tpr = new List<float>();
        private List<float> _fpr = new List<float>();
        private List<float> _fnr = new List<float>();

        private Timer _searchTime = new Timer();
        private List<float> _searchTimeList = new List<float>();
        private List<float> _threshold = new List<float>();

        public Calculation() 
        {
            GetServices();
        }

        public void Calculate() 
        {
            var fpirCriterion = _criterion.Criterion.Record.Fpir;
            var thresholdCriterion = _criterion.Criterion.Record.Threshold;

            if (fpirCriterion == 0)
                CalculateWithManuallyThreshold(thresholdCriterion);

            if (thresholdCriterion == 0)
                CalculateOptimum();
        }

        private void CalculateWithManuallyThreshold(float threshold)
        {
            GetParameters(threshold);

            MessageBox.Show($"Вычисления успешно выполнены!\n" +
                "Запись сохранена во вкладке главной страницы \"История работы\"\n" +
                "TPIR = " + Format(_tpir.FirstOrDefault()) + "\n" +
                "FPIR = " + Format(_fpir.FirstOrDefault()) + "\n" +
                "FNIR = " + Format(_fnir.FirstOrDefault()) + "\n" +
                "Время работы детектора = " + Format(_image.Image.DetectionTime.Mean()) + "\n" +
                "Время генерации вектора = " + Format(_image.Image.GenerateVectorTime.Mean()) + "\n" +
                "Время поиска по базе = " + Format(_searchTimeList.Mean()) + "\n" +
                "Затраты вычислительных ресурсов = " + 
                Format(CpuCounter.CpuCounterList.Mean() / Environment.ProcessorCount) + "%");

            BuildTimeCharts();

            _history.Save(_criterion.Criterion.Record.AlgorithmName, _metric.Name, _fpir.FirstOrDefault(),
                _fnir.FirstOrDefault(), _tpir.FirstOrDefault(), _image.Image.DetectionTime.Mean(),
                _image.Image.GenerateVectorTime.Mean(), _searchTimeList.Mean(),
                CpuCounter.CpuCounterList.Mean() / Environment.ProcessorCount);
        }

        private void CalculateOptimum()
        {
            float threshold = 0.0f;

            while (threshold <= 1)
            {
                _threshold.Add(threshold);
                GetParameters(threshold);
                threshold += 0.005f;
            }

            ChartBuilder.BuildGraph(_fpr.ToArray(), _tpr.ToArray(), "ROC-кривая", "ROC-кривая", "FPR", "TPR");
            ChartBuilder.BuildGraph(_fpir.ToArray(), _fnir.ToArray(), "DET-кривая", "DET-кривая", "FPIR", "FNIR");
            BuildTimeCharts();

            var thresholdByFpir = CalculateThresholdByDet();

            _history.Save(_criterion.Criterion.Record.AlgorithmName, _metric.Name, 
                _fpir.ElementAt(_threshold.IndexOf(thresholdByFpir)),
                _fnir.ElementAt(_threshold.IndexOf(thresholdByFpir)), 
                _tpir.ElementAt(_threshold.IndexOf(thresholdByFpir)), 
                _image.Image.DetectionTime.Mean(),
                _image.Image.GenerateVectorTime.Mean(), _searchTimeList.Mean(),
                CpuCounter.CpuCounterList.Mean() / Environment.ProcessorCount);
        }

        private void GetParameters(float threshold)
        {
            int totalRegisteredUsers = 0;
            int totalUnRegisteredUsers = 0;
            int reallyRegisteredButDontAcceptedByAlgorithm = 0;
            int reallyRegisteredAndAcceptedByAlgorithm = 0;
            int reallyUnRegisteredButAcceptedByAlgorithm = 0;
            int reallyUnRegisteredAndDontAcceptedByAlgorithm = 0;

            bool isRegistered = false;
            bool isRegisteredAndAccepted = false;
            bool isUnRegisteredAndAccepted = false;

            var imageDataTestValues = _image.Image.Test.Values;
            var imageDataBaseValues = _image.Image.Base.Values;

            foreach (var imageDataTestValue in imageDataTestValues)
            {
                _searchTime.Start();

                isRegistered = imageDataBaseValues.Any(x => x.Id == imageDataTestValue.Id);

                foreach(var imageDataBaseValue in imageDataBaseValues)
                {
                    var similarity = _metric.CompareVectors(imageDataTestValue.Vector, imageDataBaseValue.Vector);

                    CpuCounter.AddCpuTick();

                    if (isRegistered)
                    {
                        if (similarity >= threshold)
                        {
                            isRegisteredAndAccepted = true;
                            break;
                        }
                    }
                    else
                    {
                        if (similarity >= threshold)
                        {
                            isUnRegisteredAndAccepted = true;
                            break;
                        }
                    }
                }
                CpuCounter.AddCpuTick();

                _searchTime.Stop();
                _searchTimeList.Add(_searchTime.ElapsedTime);
                _searchTime.Reset();

                if (isRegistered)
                {
                    totalRegisteredUsers++;

                    if (!isRegisteredAndAccepted)
                        reallyRegisteredButDontAcceptedByAlgorithm++;
                }
                else
                {
                    if (isUnRegisteredAndAccepted)
                        reallyUnRegisteredButAcceptedByAlgorithm++;
                }

                isRegistered = false;
                isRegisteredAndAccepted = false;
                isUnRegisteredAndAccepted = false;
            }

            totalUnRegisteredUsers = _image.Image.Test.Count - totalRegisteredUsers;
            reallyRegisteredAndAcceptedByAlgorithm = totalRegisteredUsers - reallyRegisteredButDontAcceptedByAlgorithm;
            reallyUnRegisteredAndDontAcceptedByAlgorithm = totalUnRegisteredUsers - reallyUnRegisteredButAcceptedByAlgorithm;

            float fnir = reallyRegisteredButDontAcceptedByAlgorithm / (float)_image.Image.Test.Count;
            float fpir = reallyUnRegisteredButAcceptedByAlgorithm / (float)_image.Image.Test.Count;
            float tpir = 1 - fnir;

            float tpr = reallyRegisteredAndAcceptedByAlgorithm / (float)totalRegisteredUsers;
            float fpr = reallyUnRegisteredButAcceptedByAlgorithm / (float)totalUnRegisteredUsers;
            float fnr = 1 - tpr;

            _tpr.Add(tpr);
            _fpr.Add(fpr);
            _fnr.Add(fnr);

            _tpir.Add(tpir);
            _fpir.Add(fpir);
            _fnir.Add(fnir);
        }

        private float CalculateThresholdByDet()
        {
            var closestFpir = _fpir.OrderBy(x => _fnir.Select(y => Math.Abs(x - y)).Min()).First();

            var threshold = _threshold.ElementAt(_fpir.IndexOf(closestFpir));

            MessageBox.Show("Оптимальный порог равен " + Format(threshold) + "\n" +
                            "При этом FPIR равен " + Format(closestFpir));

            return threshold;
        }

        private void BuildTimeCharts()
        {
            ChartBuilder.BuildGraph(Converter.GenerateFloatArrayWithIntNumbers(_image.Image.DetectionTime.Count),
                _image.Image.DetectionTime.ToArray(), "Время детектирования", "Время детектирования",
                "Номер замера", "Время");

            ChartBuilder.BuildGraph(Converter.GenerateFloatArrayWithIntNumbers(_image.Image.GenerateVectorTime.Count),
                _image.Image.GenerateVectorTime.ToArray(), "Время генерации вектора", "Время генерации вектора",
                "Номер замера", "Время");

            ChartBuilder.BuildGraph(Converter.GenerateFloatArrayWithIntNumbers(_searchTimeList.Count),
                _searchTimeList.ToArray(), "Время поиска по базе", "Время поиска по базе",
                "Номер замера", "Время");
        }

        private string Format(double value)
        {
            return string.Format("{0:0.###}", value).Replace(',', '.');
        }

        private void GetServices() 
        {
            _criterion = ServiceLocator.Take<ICriterionProvider>() as CriterionProvider;
            _image = ServiceLocator.Take<IImageProvider>() as ImageProvider;
            _history = ServiceLocator.Take<IHistoryProvider>() as HistoryProvider;
            _metric = ServiceLocator.Take<IMetric>();
        }
    }
}
