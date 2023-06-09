using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.ImageLogic
{
    internal class MatExtension
    {
        private Mat _image;
        private string _fileName;

        public Mat Image => _image;
        public string FileName => _fileName;

        public MatExtension(Mat image, string fileName)
        {
            _image = image;
            _fileName = fileName;
        }
    }
}
