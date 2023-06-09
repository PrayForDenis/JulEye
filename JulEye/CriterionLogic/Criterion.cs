using JulEye.Helpers;
using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JulEye.CriterionLogic
{
    internal class Criterion : ICriterionViewer
    {
        private Record _record;

        public Record Record => _record;

        public bool Save(string algorithmName, string fpir, string threshold)
        {
            float fpirValue;
            float thresholdValue;

            if (string.IsNullOrEmpty(fpir))
            {
                bool isValidThreshold = InputController.CheckDoubleCriterion(threshold, out thresholdValue);
                if (isValidThreshold)
                {
                    _record = new Record(algorithmName, 0, thresholdValue);
                    return true;
                }
                MessageBox.Show("Значение критерия \"Порог схожести\" задано неверно!");
                return false;
            }

            bool isVaildFpir = InputController.CheckDoubleCriterion(fpir, out fpirValue);
            if (isVaildFpir)
            {
                _record = new Record(algorithmName, fpirValue, 0);
                return true;
            }
            MessageBox.Show("Значение критерия \"FPIR\" задано неверно!");
            return false;
        }

        public void Clear()
        {
            _record = null;
        }

        public bool IsClear() 
        {
            return _record == null;
        }
    }
}
