using _3DProjection.Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3DProjection.Helpers
{
    public class DrawManager
    {
        private Canvas canvas;
        private Object3D object3d;

        private DrawManager()
        {
        }

        private static DrawManager instance;

        public static DrawManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DrawManager();
                }

                return instance;
            }
        }

        public void SetUp(Canvas canv)
        {
            this.canvas = canv;
            this.object3d = new Object3D();
        }

        public void AddNode(double x, double y, double z)
        {
            Node node = this.object3d.AddNode(x, y, z);
            this.DrawNodeOZProjection(node);
        }

        private void DrawNodeOZProjection(Node node)
        {
            Ellipse circle = new Ellipse()
            {
                Width = 10,
                Height = 10,
                Fill = (Brush)(new System.Windows.Media.BrushConverter()).ConvertFromString("#4527a0"),
                StrokeThickness = 0
            };

            this.canvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)node.X);
            circle.SetValue(Canvas.TopProperty, (double)node.Z);
        }

    }
}
