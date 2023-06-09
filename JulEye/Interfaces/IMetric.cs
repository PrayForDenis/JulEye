using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Interfaces
{
    public interface IMetric
    {
        string Name { get; }

        float CompareVectors(float[] vector1, float[] vector2);
    }
}
