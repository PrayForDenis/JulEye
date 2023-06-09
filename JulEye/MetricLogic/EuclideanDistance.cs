using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JulEye.MetricLogic
{
    internal class EuclideanDistance : IMetric
    {
        private const string _name = "Евклидово расстояние";

        public string Name => _name;

        public float CompareVectors(float[] vector1, float[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                MessageBox.Show("Размеры векторов должны совпадать!");
                return -1;
            }

            float sum = 0;

            for (int i = 0; i < vector1.Length; i++)
            {
                sum += (vector1[i] - vector2[i]) * (vector1[i] - vector2[i]);
            }

            float distance = (float) Math.Sqrt(sum);
            float similarity = 1 / (1 + distance);

            return similarity;
        }
    }
}
