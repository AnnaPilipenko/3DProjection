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

        public Node AddNode(double x, double y, double z)
        {
            Node node = NodeFactory.GetNode3D(x, y, z);
            this.Nodes.Add(node);
            return node;
        }

        public List<Node> GetOZProjection()
        {
            List<Node> nodes = new List<Node>();
            foreach (Node node in this.Nodes)
            {
                nodes.Add(NodeFactory.GetNode2D(node.X, node.Z));
            }

            return nodes;
        }
    }
}
