using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Interfaces
{
    public interface ICriterionProvider : IProvider
    {
        bool Save(string algorithmName, string fpir = "", string threshold = "");

        void Clear();

        bool IsClear();
    }
}
