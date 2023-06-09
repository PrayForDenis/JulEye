using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.CriterionLogic
{
    internal class CriterionProvider : ICriterionProvider
    {
        private Criterion _criterion;

        public ICriterionViewer Criterion => _criterion;

        public CriterionProvider()
        {
            _criterion = new Criterion();
        }

        public bool Save(string algorithmName, string fpir = "", string threshold = "") 
        {
            return _criterion.Save(algorithmName, fpir, threshold);
        }

        public void Clear() 
        {
            _criterion.Clear();
        }

        public bool IsClear() 
        {
            return _criterion.IsClear();
        }
    }
}
