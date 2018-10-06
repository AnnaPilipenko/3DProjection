using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
