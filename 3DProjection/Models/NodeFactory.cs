using _3DProjection.Helpers;
using System.Collections.Generic;

namespace _3DProjection.Models
{
    public class NodeFactory
    {
        private static List<Node> Nodes3D { get; set; } = new List<Node>();


        public static Node GetNode3D(double x, double y, double z)
        {
            Node node = Nodes3D.Find(n => n.Equals(x, y, z));
            return node ?? new Node(x, y, z);
        }
    }
}
