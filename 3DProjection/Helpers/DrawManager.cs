using _3DProjection.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3DProjection.Helpers
{
    public class DrawManager
    {
        private Canvas canvas;
        private Object3D object3d;

        private Point startDrawingMousePosition;
        private bool isDrawingLine = false;
        private Line currentDrawingLine;

        private Brush brush = (Brush)(new System.Windows.Media.BrushConverter()).ConvertFromString("#4527a0");

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
            
            this.canvas.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(this.Canvas_MouseLeftButtonDown), true);
            this.canvas.MouseMove += this.Canvas_MouseMove;
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
                Width = 8,
                Height = 8,
                Fill = this.brush,
                StrokeThickness = 0
            };

            this.canvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)node.X);
            circle.SetValue(Canvas.TopProperty, (double)node.Z);
        }

        private void DrawLine(double x1, double y1, double x2, double y2)
        {
            Line line = new Line();
            this.currentDrawingLine = line;
            line.Stroke = this.brush;
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;

            line.StrokeThickness = 2;
            this.canvas.Children.Add(line);
        }

        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IInputElement clickedElement = Mouse.DirectlyOver;
            if (!this.isDrawingLine)
            {
                if (clickedElement is Ellipse)
                {
                    this.isDrawingLine = true;
                    this.startDrawingMousePosition = e.GetPosition((Canvas)sender);
                }
            }
            else
            {
                this.isDrawingLine = false;
                if (clickedElement is Ellipse && this.currentDrawingLine != null)
                {
                    this.canvas.Children.Remove(this.currentDrawingLine);
                    this.DrawLine(this.startDrawingMousePosition.X, this.startDrawingMousePosition.Y, e.GetPosition((Canvas)sender).X, e.GetPosition((Canvas)sender).Y);
                    this.currentDrawingLine = null;
                }
                else if (this.currentDrawingLine != null)
                {
                    this.canvas.Children.Remove(this.currentDrawingLine);
                }
            }
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.isDrawingLine)
            {
                if (this.currentDrawingLine != null)
                {
                    this.canvas.Children.Remove(this.currentDrawingLine);
                }

                this.DrawLine(this.startDrawingMousePosition.X, this.startDrawingMousePosition.Y, e.GetPosition((Canvas)sender).X, e.GetPosition((Canvas)sender).Y - 10);
            }
        }


    }
}
