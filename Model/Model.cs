using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Keras.Models;
using Keras.PreProcessing.Image;
using Keras.Layers;

namespace Model
{
    internal class Model
    {
        public void CreateModel()
        {
            // Определяем модель нейронной сети
            var model = new Sequential();
            model.Add(new Conv2D(32, new Tuple<int, int>(3, 3), activation: "relu", input_shape: (160, 160, 3)));
            model.Add(new MaxPooling2D(new Tuple<int, int>(2, 2)));
            model.Add(new Conv2D(64, new Tuple<int, int>(3, 3), activation: "relu"));
            model.Add(new MaxPooling2D(new Tuple<int, int>(2, 2)));
            model.Add(new Conv2D(128, new Tuple<int, int>(3, 3), activation: "relu"));
            model.Add(new MaxPooling2D(new Tuple<int, int>(2, 2)));
            model.Add(new Conv2D(128, new Tuple<int, int>(3, 3), activation: "relu"));
            model.Add(new MaxPooling2D(new Tuple<int, int>(2, 2)));
            model.Add(new Flatten());
            model.Add(new Dense(512, activation: "relu"));
            model.Add(new Dense(128));

            // Компилируем модель нейронной сети
            model.Compile(optimizer: "rmsprop", loss: "mse", metrics: new[] { "accuracy" });

            // Загружаем изображения
            var imageGenerator = new ImageDataGenerator(rescale: (float?)(1.0 / 255));
            var trainData = imageGenerator.FlowFromDirectory(
                directory: "D:\\Games\\keras_to_tensorflow\\Humans",
                target_size: new Tuple<int, int>(160, 160),
                batch_size: 32,
                class_mode: "categorical"
            );

            // Обучаем модель нейронной сети
            model.FitGenerator(
                generator: trainData,
                steps_per_epoch: 7219 / 32,
                epochs: 10
            );

            model.Save("model.h5");
        }
    }
}
