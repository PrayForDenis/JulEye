using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Helpers
{
    internal static class InputController
    {
        public static bool CheckDoubleCriterion(string criterion, out float criterionValue)
        {
            bool isValidCriterion;

            isValidCriterion = (float.TryParse(criterion, out criterionValue) &&
                                criterionValue >= 0.0001d && criterionValue <= 1.0000d);

            return isValidCriterion;
        }
    }
}
