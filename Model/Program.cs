using Keras.Models;
using Keras.Layers;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using Keras.PreProcessing.Image;
using Numpy;
using OpenCvSharp;
using System.Runtime.InteropServices;
using System;
using System.Globalization;
using Model;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.Arm;

//var model = Keras.Models.Model.LoadModel("facenet.h5");

//// Получаем 128 вектор признаков для изображения
//var image = ImageUtil.LoadImg("C:\\Users\\днс\\OneDrive\\Рабочий стол\\Последний танец\\Диплом\\data_face\\1\\1.1.jpg");

// NDarray img = ImageUtil.ImageToArray(image);

//Mat im = new Mat();
//Mat image1 = new Mat("C:\\Users\\днс\\OneDrive\\Рабочий стол\\Последний танец\\Диплом\\data_face\\1\\1.1.jpg");

//var faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
//var faces = faceCascade.DetectMultiScale(image1);
//if (faces.Length == 0)
//{
//    Console.WriteLine("Лицо не было обнаружено!");
//}
//var face = faces.First();
//var croppedImg = new Mat(image1, face);
//Cv2.Resize(croppedImg, croppedImg, new Size(160, 160));
//croppedImg.ConvertTo(croppedImg, MatType.CV_32F, 1.0 / 255.0);


//Cv2.Resize(croppedImg, im, new Size(160, 160));
//byte[] imgBytes = new byte[im.Total() * im.ElemSize()]; // создаем массив byte
//unsafe
//{
//    fixed (byte* ptr = imgBytes)
//    {
//        IntPtr imgData = im.Data;
//        for (int i = 0; i < im.Total() * im.ElemSize(); i++)
//        {
//            *(ptr + i) = Marshal.ReadByte(imgData, i); // копируем данные изображения в массив byte
//        }
//    }
//}
//NDarray ndImage = new NDarray(imgBytes).reshape(1, 160, 160, 3);
//Console.WriteLine(ndImage.shape.ToString());
//var features = model.Predict(ndImage);




//// Скрипт для создания JSON описаний
//int id = 0;
//ImageData data = new ImageData();

//String mypath = "D:\\Games\\data\\test";
//var filePaths = Directory
//    .GetFiles(mypath, "*", SearchOption.AllDirectories)
//    .ToList();

//foreach (var filePath in filePaths)
//{
//    var file = Path.GetFileName(filePath);
//    ImageDataFields imageData = new ImageDataFields(id.ToString(), file, new float[] { 0 });
//    data.Data.Add(imageData);
//    string output = JsonConvert.SerializeObject(data);
//    string json = "[" + output + "]";
//    System.IO.File.WriteAllText(@$"{mypath}\\point_{id}.json", json);
//    id++;
//    data.Data.Clear();
//}





// Скрипт для генерации фоток в папку
//int id = 0;

//for (int i = 0; i <= 999; i++)
//{
//    String mypath = $"D:\\Games\\data\\test\\{i}";
//    var filePaths = Directory
//        .GetFiles(mypath, "*", SearchOption.AllDirectories)
//        .ToList();

//    foreach (var filePath in filePaths)
//    {
//        var file = Path.GetFileName(filePath);
//        File.Move(filePath, $"D:\\Games\\data\\test\\{id}.{file.Split('.')[1]}");
//        id++;
//    }
//}