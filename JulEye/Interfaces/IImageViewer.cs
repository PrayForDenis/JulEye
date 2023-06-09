using JulEye.Helpers;
using JulEye.ImageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Interfaces
{
    internal interface IImageViewer
    {
        IReadOnlyDictionary<MatExtension, ImageDataFields> Base { get; }
        IReadOnlyDictionary<MatExtension, ImageDataFields> Test { get; }
        IReadOnlyList<float> DetectionTime { get; }
        IReadOnlyList<float> GenerateVectorTime { get; }
    }
}
