using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.ImageLogic
{
    internal class ImageProvider : IImageProvider
    {
        private Image _image;

        public IImageViewer Image => _image;

        public ImageProvider()
        {
            _image = new Image();
        }

        public bool Save(string file, bool inBase = false) 
        {
            return _image.SaveImage(file, inBase);
        }

        public bool Save(string file, bool inBase = false, bool isVector = false) 
        {
            return _image.SaveVector(file, inBase);
        }

        public bool IsNeedToGenerateVectors()
        {
            return _image.IsNeedToGenerateVectors();
        }

        public void Clear() 
        {
            _image.Clear();
        }

        public bool IsClear()
        {
            return _image.IsClear();
        }

        public bool GenerateVectors(string modelFile) 
        {
            return _image.GenerateVectors(modelFile);
        }
    }
}
