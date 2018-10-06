using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DProjection.Helpers
{
    public static class ExtensionMethod
    {
        public static bool Equals(double number1, double number2, double? accurancy = null)
        {
            accurancy = accurancy ?? 0.01;
            return Math.Abs(number1 - number2) < accurancy;
        }
    }
}
