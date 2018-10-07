using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DProjection.Models
{
    public enum RotationLineEnum
    {
        AboutOX,
        AboutOY,
        AboutOZ
    }

    public static class Extensions
    {
        /// <summary>
        /// Returns rotation matrix by specified rotation line
        /// </summary>
        /// <param name="rotationLineEnum">Specifies line of rotation</param>
        /// <param name="alpha">An angle measured in radians</param>
        /// <returns></returns>
        public static double[,] GetRotationMatrix(this RotationLineEnum rotationLineEnum, double alpha)
        {
            double[,] rotationMatrix = null;
            switch (rotationLineEnum)
            {
                case RotationLineEnum.AboutOX:
                    rotationMatrix = RotationMatrix3D.GetXRotationMatrix(alpha);
                    break;
                case RotationLineEnum.AboutOY:
                    rotationMatrix = RotationMatrix3D.GetYRotationMatrix(alpha);
                    break;
                case RotationLineEnum.AboutOZ:
                    rotationMatrix = RotationMatrix3D.GetZRotationMatrix(alpha);
                    break;
            }

            return rotationMatrix;
        }
    }

    class Object3D
    {
        public List<Node> Nodes { get; private set; } = new List<Node>();
        private Node MassCenter { get; set; } = null;

        public Node AddNode(double x, double y, double z)
        {
            Node node = NodeFactory.GetNode3D(x, y, z);
            this.Nodes.Add(node);
            return node;
        }

        public void RemoveNode(Node node)
        {
            if (this.Nodes.Contains(node))
            {
                for (int i = 0; i < this.Nodes.Count; i++)
                {
                    if (this.Nodes[i].NeighborNodes.Contains(node))
                    {
                        this.Nodes[i].NeighborNodes.Remove(node);
                    }
                }

                this.Nodes.Remove(node);
            }
        }

        public Node GetMassCenter(bool update = false)
        {
            if (this.MassCenter == null || update)
            {
                double xCenter = 0, yCenter = 0, zCenter = 0;
                foreach (Node node in this.Nodes)
                {
                    xCenter += node.X;
                    yCenter += node.Y;
                    zCenter += node.Z;
                }

                xCenter /= this.Nodes.Count;
                yCenter /= this.Nodes.Count;
                zCenter /= this.Nodes.Count;

                this.MassCenter = NodeFactory.GetNode3D(xCenter, yCenter, zCenter);
                return this.MassCenter;
            }
            else
            {
                return this.MassCenter;
            }
        }

        /// <summary>
        /// Rotate each node about specified axis by the specified angle
        /// </summary>
        /// <param name="alpha">An angle measured in radians</param>
        ///  <param name="rotateAboutEnum">Specifies line of rotation (see RotationLineEnum)</param>

        public void Rotate(double alpha, RotationLineEnum rotationLineEnum)
        {
            //// Update mass center if it is null
            this.GetMassCenter(false);

            //// Move the origin to the mass center
            Node prevOrigin = this.MoveTheOrigin(this.MassCenter.X, this.MassCenter.Y, this.MassCenter.Z);

            //// Specify rotation matrix 
            double[,] rotationMatrix = rotationLineEnum.GetRotationMatrix(alpha);

            //// Rotate each node about specified axis axis
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                double[] result = Matrix.Multiply(rotationMatrix, this.Nodes[i].ToVector());
                this.Nodes[i].SetValuesFromVector(result);
            }

            //// Revert (move the origin to previous coordinate)
            this.MoveTheOrigin(prevOrigin.X, prevOrigin.Y, prevOrigin.Z);

            //// Update mass center
            this.GetMassCenter(true);
        }

        /// <summary>
        /// Move the origin of a Euclidean space to the specified coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>Returns coordinate of previous origin</returns>

        public Node MoveTheOrigin(double x, double y, double z)
        {
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                this.Nodes[i].X -= x;
                this.Nodes[i].Y -= y;
                this.Nodes[i].Z -= z;
            }

            this.GetMassCenter(true);
            Node prevOrigin = NodeFactory.GetNode3D(-x, -y, -z);

            return prevOrigin;
        }
    }
}
