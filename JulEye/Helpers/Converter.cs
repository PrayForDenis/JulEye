using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Helpers
{
    internal static class Converter
    {
        public static float[] ConvertNDarrayToFloatArray(NDarray features) 
        {
            List<float> predictValues = new List<float>();
            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.InstalledUICulture;
            System.Globalization.NumberFormatInfo ni = (System.Globalization.NumberFormatInfo)ci.NumberFormat.Clone();
            ni.NumberDecimalSeparator = ".";

            var strValues = features
                            .ToString()
                            .Replace('[', ' ')
                            .Replace(']', ' ')
                            .Split(new char [] {' '} , StringSplitOptions.RemoveEmptyEntries);
            foreach (string strValue in strValues)
            {
                var value = float.Parse(strValue, ni);
                predictValues.Add(value);
            }
            
            return predictValues.ToArray();
        }

        public static float[] GenerateFloatArrayWithIntNumbers(int count)
        {
            var ints = Enumerable.Range(0, count);
            List<float> xValuesGenerate = new List<float>();

            foreach (var i in ints)
            {
                xValuesGenerate.Add(i);
            }

            return xValuesGenerate.ToArray();
        }
    }
}
