using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DProjection.Models
{
    public class Matrix
    {
        public static double[,] Multiply(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");

            double[,] result = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return result;
        }

        public static double[] Multiply(double[,] b, double[] a)
        {
            double[,] c = new double[a.Length, 1];
            for (int i = 0; i < a.Length; i++)
            {
                c[i, 0] = a[i];
            }

            double[,] result = Matrix.Multiply(b, c);
            double[] newResult = new double[result.GetLength(0)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                newResult[i] = result[i, 0];
            }

            return newResult;
        }
    }
}
