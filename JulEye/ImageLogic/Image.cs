using JulEye.HistoryLogic;
using OpenCvSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using JulEye.Interfaces;
using OpenCvSharp.Internal.Vectors;
using Keras;
using TensorFlow;
using Keras.Models;
using Keras.PreProcessing.Image;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using Numpy;
using JulEye.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace JulEye.ImageLogic
{
    internal class Image : IImageViewer
    {
        private BaseModel _model;
        private Dictionary<MatExtension, ImageDataFields> _base = new Dictionary<MatExtension, ImageDataFields>();
        private Dictionary<MatExtension, ImageDataFields> _test = new Dictionary<MatExtension, ImageDataFields>();
        private Timer _detectionTime = new Timer();
        private Timer _generateVectorTime = new Timer();
        private List<float> _detectionTimeList = new List<float>();
        private List<float> _generateVectorTimeList = new List<float>();

        public IReadOnlyDictionary<MatExtension, ImageDataFields> Base => _base;
        public IReadOnlyDictionary<MatExtension, ImageDataFields> Test => _test;
        public IReadOnlyList<float> DetectionTime => _detectionTimeList;
        public IReadOnlyList<float> GenerateVectorTime => _generateVectorTimeList;

        public bool SaveImage(string file, bool inBase) 
        {
            try
            {
                var image = new Mat(file);
                var fileName = System.IO.Path.GetFileName(file);
                var matExt = new MatExtension(image, fileName);
                if (inBase)
                    AddKey(matExt, _base);
                else
                    AddKey(matExt, _test);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка при загрузке изображения!\n" + e.Message);
                return false;
            }
        }

        public bool SaveVector(string file, bool inBase = false) 
        {
            try
            {
                string fileText = System.IO.File.ReadAllText(file);
                var collection = JsonConvert.DeserializeObject<List<ImageData>>(fileText);
                ImageData data = collection.FirstOrDefault();
                foreach (var fields in data.Data)
                {
                    if (inBase)
                    {
                        if (!AddValueByKey(fields, _base))
                            continue;
                    }
                    else
                    {
                        if (!AddValueByKey(fields, _test))
                            continue;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка при загрузке вектора!\n" + e.Message);
                return false;
            }
        }

        public bool GenerateVectors(string modelName) 
        {
            var isSuccessGenerateForBase = GenerateVectorsForStorage(_base, modelName);
            CpuCounter.AddCpuTick();
            var isSuccessGenerateForTest = GenerateVectorsForStorage(_test, modelName);

            if (isSuccessGenerateForBase && isSuccessGenerateForTest)
                return true;

            return false;
        }

        public bool IsNeedToGenerateVectors()
        {
            if (_base.Values.FirstOrDefault().Vector.Length == 1 && _test.Values.FirstOrDefault().Vector.Length == 1)
                return true;

            return false;
        }

        public void Clear() 
        {
            _base.Clear();
            _test.Clear();
            _model = null;
        }

        public bool IsClear() 
        {
            return (_base.Count == 0) && (_test.Count == 0);
        }

        private bool AddValueByKey(ImageDataFields fields, Dictionary<MatExtension, ImageDataFields> storage)
        {
            MatExtension key = storage
                               .Select(x => x.Key)
                               .ToList()
                               .FirstOrDefault(x => x.FileName.Equals(fields.Name));
            
            if (key == null)
                return false;

            if (storage.ContainsKey(key) && storage.TryGetValue(key, out ImageDataFields outFields) && outFields == null)
                storage[key] = fields;

            return true;
        }

        private void AddKey(MatExtension matExt, Dictionary<MatExtension, ImageDataFields> storage) 
        {
            if (storage.ContainsKey(matExt))
            {
                MessageBox.Show($"Изображение {matExt.FileName} уже загружено!");
                return;
            }

            storage.Add(matExt, null);
        }

        private float[] GenerateVector(Mat image, string modelFile)
        {
            try
            {
                if (_model == null)
                    _model = Keras.Models.Model.LoadModel(modelFile);

                CpuCounter.AddCpuTick();
                _generateVectorTime.Start();

                byte[] imgBytes = new byte[image.Total() * image.ElemSize()]; // создаем массив byte

                unsafe
                {
                    fixed (byte* ptr = imgBytes)
                    {
                        IntPtr imgData = image.Data;
                        for (int i = 0; i < image.Total() * image.ElemSize(); i++)
                        {
                            *(ptr + i) = Marshal.ReadByte(imgData, i); // копируем данные изображения в массив byte
                        }
                    }
                }

                CpuCounter.AddCpuTick();
                NDarray ndImage = new NDarray(imgBytes).reshape(1, 160, 160, 3);
                var features = _model.Predict(ndImage);
                var floatFeatures = Converter.ConvertNDarrayToFloatArray(features);

                _generateVectorTime.Stop();
                _generateVectorTimeList.Add(_detectionTime.ElapsedTime);
                _generateVectorTime.Reset();

                return floatFeatures;
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка при генерации вектора признаков!\n" + e.Message);
                return null;
            }
        }

        private Mat DetectFace(Mat image) 
        {
            var faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
            var faces = faceCascade.DetectMultiScale(image);

            if (faces.Length == 0)
            {
                MessageBox.Show("Лицо не было обнаружено!");
                return null;
            }

            CpuCounter.AddCpuTick();
            var face = faces.First();
            var croppedImg = new Mat(image, face);
            Cv2.Resize(croppedImg, croppedImg, new OpenCvSharp.Size(160, 160));

            return croppedImg;
        }

        private bool GenerateVectorsForStorage(Dictionary<MatExtension, ImageDataFields> storage, string modelName) 
        {
            ImageDataFields oldFields;
            ImageDataFields newFields;

            var storageCopy = new Dictionary<MatExtension, ImageDataFields>(storage);

            foreach (var key in storageCopy.Keys)
            {
                _detectionTime.Start();
                var detectedImage = DetectFace(key.Image);
                _detectionTime.Stop();
                _detectionTimeList.Add(_detectionTime.ElapsedTime);
                _detectionTime.Reset();

                if (detectedImage == null)
                    return false;

                var features = GenerateVector(detectedImage, modelName);

                if (!storage.TryGetValue(key, out oldFields))
                    return false;

                newFields = new ImageDataFields(oldFields.Id, oldFields.Name, features);
                storage[key] = newFields;
            }
            return true;
        }
    }
}
