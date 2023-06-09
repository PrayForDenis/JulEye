using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.HistoryLogic
{
    internal class Result : IData
    {
        private string _algorithmName;
        private string _metric;
        private double _fpir;
        private double _fnir;
        private double _tpir;
        private double _detectionTime;
        private double _generateVectorTime;
        private double _searchTime;
        private string _computingCost;

        public string AlgorithmName => _algorithmName;
        public string Metric => _metric;
        public double Fpir => _fpir;
        public double Fnir => _fnir;
        public double Tpir => _tpir;
        public double DetectionTime => _detectionTime;
        public double GenerateVectorTime => _generateVectorTime;
        public double SearchTime => _searchTime;
        public string ComputingCost => _computingCost;

        public Result(string algorithmName, string metric, double fpir, double fnir, double tpir,
                    double detectionTime, double generateVectorTime, double searchTime, string computingCost)
        {
            _algorithmName = algorithmName;
            _metric = metric;
            _fpir = fpir;
            _fnir = fnir;
            _tpir = tpir;
            _detectionTime = detectionTime;
            _generateVectorTime = generateVectorTime;
            _searchTime = searchTime;
            _computingCost = computingCost;
        }
    }
}