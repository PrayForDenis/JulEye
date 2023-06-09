using JulEye.Interfaces;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JulEye.MetricLogic
{
    internal class CosineDistance : IMetric
    {
        private const string _name = "Косинусное расстояние";

        public string Name => _name;
            
        public float CompareVectors(float[] vector1, float[] vector2)
        {
            if (vector1 == null || vector2 == null || vector1.Length != vector2.Length)
            {
                MessageBox.Show("Вектора не должны быть нулевыми, а также их размеры должны совпадать!");
                return -1;
            }

            float dotProduct = 0f;
            float norm1 = 0f;
            float norm2 = 0f;

            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                norm1 += vector1[i] * vector1[i];
                norm2 += vector2[i] * vector2[i];
            }

            if (norm1 <= 0 || norm2 <= 0)
            {
                MessageBox.Show("Вектора не должны быть нулевыми!");
                return -1;
            }

            float similarity = dotProduct / (float) (Math.Sqrt(norm1) * Math.Sqrt(norm2));

            return similarity;
        }
    }
}
