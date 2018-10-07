using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DProjection.Models
{
    public class RotationMatrix3D
    {
        /// <summary>
        /// Returns the rotation matrix 3x3 (about X axis) for the specified angle.
        /// </summary>
        /// <param name="alpha">An angle measured in radians</param>
        /// <returns></returns>
        public static double[,] GetXRotationMatrix(double alpha)
        {
            return new double[3, 3]
                    {
                        { 1, 0, 0 },
                        { 0, Math.Cos(alpha), -Math.Sin(alpha) },
                        { 0, Math.Sin(alpha), Math.Cos(alpha) }
                   };
        }

        /// <summary>
        /// Returns the rotation matrix 3x3 (about Y axis) for the specified angle.
        /// </summary>
        /// <param name="alpha">An angle measured in radians</param>
        /// <returns></returns>
        public static double[,] GetYRotationMatrix(double alpha)
        {
            return new double[3, 3]
                    {
                        {  Math.Cos(alpha), 0,  Math.Sin(alpha) },
                        { 0, 1, 0 },
                        {  -Math.Sin(alpha), 0, Math.Cos(alpha) }
                   };
        }

        /// <summary>
        /// Returns the rotation matrix 3x3 (about Z axis) for the specified angle.
        /// </summary>
        /// <param name="alpha">An angle measured in radians</param>
        /// <returns></returns>
        public static double[,] GetZRotationMatrix(double alpha)
        {
            return new double[3, 3]
                    {
                        {  Math.Cos(alpha), -Math.Sin(alpha),  0 },
                        { Math.Sin(alpha), Math.Cos(alpha), 0 },
                        {  0, 0, 1 }
                   };
        }
    }
}
