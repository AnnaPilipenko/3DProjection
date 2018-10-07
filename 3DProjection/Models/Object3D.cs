using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DProjection.Models
{
    class Object3D
    {
        private List<Node> Nodes { get; set; } = new List<Node>();
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

        public void RotateAboutOX()
        {

        }

        public void RotateAboutOY()
        {

        }

        public void RotateAboutOZ()
        {

        }
    }
}
