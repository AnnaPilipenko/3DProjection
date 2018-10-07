using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DProjection.Helpers
{ 
    public static class NumericExtensions
    {
        /// <summary>
        /// Convert to Radians.
        /// </summary>
        /// <param name="angle">The angle to convert to radians</param>
        /// <returns>The value in radians</returns>
        public static double ToRadians(this double angle)
        {
            return (Math.PI / 180) * angle;
        }

        /// <summary>
        /// Compare two floating-point numbers with the specified accurency
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="accurancy">Comparsion accurency (by default is 0.01)</param>
        /// <returns></returns>
        public static bool Equals(this double number1, double number2, double? accurancy = null)
        {
            accurancy = accurancy ?? 0.01;
            return Math.Abs(number1 - number2) < accurancy;
        }
    }
}
