using _3DProjection.Helpers;
using System.Collections.Generic;

namespace _3DProjection.Models
{
    public class NodeFactory
    {
        private static List<Node> Nodes3D { get; set; } = new List<Node>();

        private static List<Node> Nodes2D { get; set; } = new List<Node>();

        public static Node GetNode3D(double x, double y, double z)
        {
            Node node = Nodes3D.Find(n => ExtensionMethod.Equals(n.X, x) && ExtensionMethod.Equals(n.Y, y) && ExtensionMethod.Equals(n.Z, z));
            return node ?? new Node(x, y, z);
        }

        public static Node GetNode2D(double x, double z, double y = 0)
        {
            Node node = Nodes2D.Find(n => ExtensionMethod.Equals(n.X, x) && ExtensionMethod.Equals(n.Y, y) && ExtensionMethod.Equals(n.Z, z));
            return node ?? new Node(x, y, z);
        }
    }
}
