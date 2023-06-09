using JulEye.HistoryLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Interfaces
{
    internal interface IHistoryViewer
    {
        IReadOnlyList<Result> Results { get; }
    }
}
