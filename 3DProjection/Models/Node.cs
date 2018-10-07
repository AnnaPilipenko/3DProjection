using System;
using System.Collections.Generic;
using _3DProjection.Helpers;

namespace _3DProjection.Models
{
    public class Node
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public List<Node> NeighborNodes { get; private set; }

        internal Node(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.NeighborNodes = new List<Node>();
        }

        public void SetValuesFromVector(double[] vector)
        {
            if (vector.Length == 3)
            {
                this.X = vector[0];
                this.Y = vector[1];
                this.Z = vector[2];
            }

            throw new Exception("Не удается конверировать вектор в узел");
        }

        public double[] ToVector()
        {
            return new double[3] { this.X, this.Y, this.Z };
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Node))
            {
                Node node = (Node)obj;
                return this.X.Equals(node.X) && this.Y.Equals(node.Y) && this.Z.Equals(node.Z);
            }

            return false;
        }

        public bool Equals(double x, double y, double z)
        {
            Node node = new Node(x, y, z);
            return this.Equals(node);
        }

        public void TryAddNeighborNode(double x, double y, double z)
        {
            var node = new Node(x, y, z);            
            this.TryAddNeighborNode(node);
        }

        public void TryAddNeighborNode(Node node)
        {
            if (!this.NeighborNodes.Contains(node))
            {
                this.NeighborNodes.Add(node);

                if (!node.NeighborNodes.Contains(this))
                {
                    node.NeighborNodes.Add(this);
                }
            }
            else
            {
                throw new Exception("The node already contains this neighbor.");
            }
        }
    }
}
