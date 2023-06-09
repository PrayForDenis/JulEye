using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.CriterionLogic
{
    internal class Record : IData
    {
        private string _algorithmName;
        private float _fpir;
        private float _threshold;

        public string AlgorithmName => _algorithmName;
        public float Fpir => _fpir;
        public float Threshold => _threshold;

        public Record(string algorithmName, float fpir, float threshold)
        {
            _algorithmName = algorithmName;
            _fpir = fpir;
            _threshold = threshold;
        }
    }
}
