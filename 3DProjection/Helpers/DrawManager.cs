using _3DProjection.Models;
using System.Collections.Generic;
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
        private Dictionary<UIElement, Node> elements;

        private UIElement massCenter;

        private Brush primaryBrush = (Brush)(new System.Windows.Media.BrushConverter()).ConvertFromString("#4527a0");
        private Brush redBrush = Brushes.Red;
        private int pointWidth = 8;
        private int lineWidth = 2;

        private bool drawingLineMode = false;
        private bool removeMode = false;

        private Point startDrawingMousePosition;
        private Node startDrawingNode;
        private Line currentDrawingLine;

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
            this.elements = new Dictionary<UIElement, Node>();

            this.canvas.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(this.Canvas_MouseDown), true);
            this.canvas.MouseMove += this.Canvas_MouseMove;
        }

        public Node AddNode(double x, double y, double z)
        {
            Node node = this.object3d.AddNode(x, y, z);
            var circle = this.DrawNodeOZProjection(node);
            this.elements.Add(circle, node);

            this.RedrawMassCenter();
            return node;
        }

        public void Clear()
        {
            this.canvas.Children.Clear();
            this.elements.Clear();
            this.object3d = new Object3D();
            this.drawingLineMode = false;
            this.currentDrawingLine = null;
            this.removeMode = false;
            this.massCenter = null;
        }

        public void RemoveModeOn()
        {
            this.removeMode = true;
            Mouse.OverrideCursor = Cursors.No;
        }

        public void RemoveModeOff()
        {
            this.removeMode = false;
            Mouse.OverrideCursor = null;
        }

        public void DrawSmth()
        {
            Node node1 = this.AddNode(100, 100, 100);
            Node node2 = this.AddNode(300, 100, 100);
            Node node3 = this.AddNode(200, 300, 100);
            Node node4 = this.AddNode(200, 200, 300);

            this.AddEdge(node1, node2);
            this.AddEdge(node1, node3);
            this.AddEdge(node1, node4);

            this.AddEdge(node2, node3);
            this.AddEdge(node2, node4);

            this.AddEdge(node3, node4);
        }

        private void RedrawMassCenter()
        {
            if (this.massCenter != null)
            {
                this.canvas.Children.Remove(this.massCenter);
            }

            if (this.elements.Count >= 3)
            {            
                Node massCenterNode = this.object3d.GetMassCenter(true);
                this.massCenter = this.DrawNodeOZProjection(massCenterNode, this.redBrush);
            }
        }

        private Ellipse DrawNodeOZProjection(Node node, Brush brush = null)
        {
            if (brush == null)
            {
                brush = this.primaryBrush;
            }

            Ellipse circle = new Ellipse()
            {
                Width = this.pointWidth,
                Height = this.pointWidth,
                Fill = brush,
                StrokeThickness = 0
            };

            this.canvas.Children.Add(circle);
            Canvas.SetZIndex(circle, 10);

            circle.SetValue(Canvas.LeftProperty, (double)node.X);
            circle.SetValue(Canvas.TopProperty, (double)node.Z);

            return circle;
        }

        private void AddEdge(Node node1, Node node2)
        {
            if (this.drawingLineMode)
            {
                this.DrawLine(this.startDrawingNode.X + this.pointWidth / 2, this.startDrawingNode.Z + this.pointWidth / 2, node2.X + this.pointWidth / 2, node2.Z + this.pointWidth / 2);
            }
            else
            {
                this.DrawLine(node1.X + this.pointWidth / 2, node1.Z + this.pointWidth / 2, node2.X + this.pointWidth / 2, node2.Z + this.pointWidth / 2);
            }

            node1.TryAddNeighborNode(node2);
        }

        private void DrawLine(double x1, double y1, double x2, double y2)
        {
            Line line = new Line();
            this.currentDrawingLine = line;
            line.Stroke = this.primaryBrush;
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;

            line.StrokeThickness = this.lineWidth;
            this.canvas.Children.Add(line);
            Canvas.SetZIndex(line, 0);
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IInputElement clickedElement = Mouse.DirectlyOver;
            if (this.removeMode)
            {
                this.object3d.RemoveNode(this.elements[(UIElement)clickedElement]);
                this.canvas.Children.Remove((UIElement)clickedElement);
                this.RedrawMassCenter();
            }
            else
            {
                if (!this.drawingLineMode)
                {
                    if (clickedElement is Ellipse)
                    {
                        this.drawingLineMode = true;
                        Mouse.OverrideCursor = Cursors.Pen;
                        var node = this.elements[(UIElement)clickedElement];
                        this.startDrawingNode = this.elements[(UIElement)clickedElement]; // e.GetPosition((Canvas)sender);
                        this.startDrawingMousePosition = e.GetPosition((Canvas)sender);
                    }
                }
                else
                {
                    if (clickedElement is Ellipse && this.currentDrawingLine != null)
                    {
                        this.canvas.Children.Remove(this.currentDrawingLine);
                        
                        var endNode = this.elements[(UIElement)clickedElement];
                        this.AddEdge(this.startDrawingNode, endNode);
                        this.currentDrawingLine = null;
                    }
                    else if (this.currentDrawingLine != null)
                    {
                        Mouse.OverrideCursor = null;
                        this.canvas.Children.Remove(this.currentDrawingLine);
                    }

                    this.drawingLineMode = false;
                }
            }
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.drawingLineMode)
            {
                if (this.currentDrawingLine != null)
                {
                    this.canvas.Children.Remove(this.currentDrawingLine);
                }

                this.DrawLine(this.startDrawingMousePosition.X, this.startDrawingMousePosition.Y, e.GetPosition((Canvas)sender).X, e.GetPosition((Canvas)sender).Y - 10);
            }
            else if(!this.removeMode)
            {
                IInputElement element = Mouse.DirectlyOver;
                if (element is Ellipse)
                {
                    Mouse.OverrideCursor = Cursors.Hand;
                }
                else
                {
                    Mouse.OverrideCursor = null;
                }
            }
        }
    }
}
