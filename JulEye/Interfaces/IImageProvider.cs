using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Interfaces
{
    public interface IImageProvider : IProvider
    {
        bool Save(string file, bool inBase = false);

        bool Save(string file, bool inBase = false, bool isVector=false);

        void Clear();

        bool IsClear();

        bool IsNeedToGenerateVectors();

        bool GenerateVectors(string modelFile);
    }
}
