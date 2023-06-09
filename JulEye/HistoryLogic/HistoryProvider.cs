using JulEye.Interfaces;
using JulEye.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace JulEye.HistoryLogic
{
    internal class HistoryProvider : IHistoryProvider
    {
        private readonly string _fileName = "Resources\\History.txt";
        private History _history;

        public IHistoryViewer History => _history;

        public HistoryProvider()
        {
            _history = new History();
        }

        public IReadOnlyList<IData> Open()
        {
            return _history.LoadFromFile(_fileName);
        }

        public void Save(string algorithmName, string metric, double fpir, double fnir, double tpir,
                        double detectionTime, double generateVectorTime, double searchTime, double computingCost)
        {
            _history.Save(algorithmName, metric, fpir, fnir, tpir, detectionTime, 
                            generateVectorTime, searchTime, computingCost, _fileName);
        }

        public void Clear()
        {
            _history.Clear();
        }
    }
}
