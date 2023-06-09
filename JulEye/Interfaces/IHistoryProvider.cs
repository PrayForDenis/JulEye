using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Interfaces
{
    public interface IHistoryProvider : IProvider
    {
        IReadOnlyList<IData> Open();

        void Save(string algorithmName, string metric, double fpir, double fnir, double tpir,
                        double detectionTime, double generateVectorTime, double searchTime, double computingCost);

        void Clear();
    }
}
