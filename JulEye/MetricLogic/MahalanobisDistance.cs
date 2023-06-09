using JulEye.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace JulEye.MetricLogic
{
    internal class MahalanobisDistance : IMetric
    {
        private const string _name = "Расстояние Махаланобиса";

        public string Name => _name;

        public float CompareVectors(float[] vector1, float[] vector2)
        {
            Matrix<float> covarianceMatrix = Matrix<float>.Build.DenseOfRowArrays(GetCovariance(vector1, vector2));

            Matrix<float> inverseCovarianceMatrix = covarianceMatrix.Inverse();

            float[] featuresDiff = vector1.Zip(vector2, (x, y) => x - y).ToArray();

            Matrix<float> inputMatrix = Matrix<float>.Build.DenseOfColumnArrays(featuresDiff);

            Matrix<float> inputMatrixT = inputMatrix.Transpose();

            float similarity = (inputMatrixT * inverseCovarianceMatrix * inputMatrix)[0, 0];

            return similarity;
        }

        private float[][] GetCovariance(float[] vector1, float[] vector2)
        {
            Matrix<float> featuresMatrix = Matrix<float>.Build.DenseOfRowArrays(vector1, vector2);

            Matrix<float> covarianceMatrix = Covariance(featuresMatrix);

            float[][] covarianceArray = covarianceMatrix.ToRowArrays();

            return covarianceArray;
        }

        private Matrix<float> Covariance(Matrix<float> matrix) 
        {
            var columnAverages = matrix.ColumnSums() / matrix.RowCount;
            var centeredColumns = matrix.EnumerateColumns().Zip(columnAverages, (col, avg) => col - avg);
            var centered = DenseMatrix.OfColumnVectors(centeredColumns);
            var normalizationFactor = matrix.RowCount == 1 ? 1 : matrix.RowCount - 1;

            return centered.TransposeThisAndMultiply(centered) / normalizationFactor;
        }
    }
}
