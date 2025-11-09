using Grafcreator.Shapes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Grafcreator
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private Point startPoint;
        private ShapeBase currentShape = null;
        private List<ShapeBase> shapes = new List<ShapeBase>();
        private string selectedTool = "Line";

        public MainWindow()
        {
            InitializeComponent();

            LineTool.Click += (s, e) => selectedTool = "Line";
            RectTool.Click += (s, e) => selectedTool = "Rectangle";
            CircleTool.Click += (s, e) => selectedTool = "Circle";
            TriangleTool.Click += (s, e) => selectedTool = "Triangle";

            DrawCanvas.MouseDown += Canvas_MouseDown;
            DrawCanvas.MouseMove += Canvas_MouseMove;
            DrawCanvas.MouseUp += Canvas_MouseUp;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            startPoint = e.GetPosition(DrawCanvas);

            switch (selectedTool)
            {
                case "Line":
                    currentShape = new Grafcreator.Shapes.Line(startPoint, startPoint);
                    break;
                case "Rectangle":
                    currentShape = new Grafcreator.Shapes.Rect(startPoint, 0, 0);
                    break;
                case "Circle":
                    currentShape = new Grafcreator.Shapes.Circle(startPoint, 0, Colors.Black, Colors.Transparent,2);
                    break;
                case "Triangle":
                    currentShape = new Grafcreator.Shapes.Triangle(startPoint, startPoint, startPoint, Colors.Black, Colors.Transparent, 2);
                    break;
            }

            if (currentShape != null)
            {
             
                currentShape.Color(Colors.Black, Colors.Transparent);

                // SetStrokeWidth
                // currentShape.SetStrokeWidth(2);

                currentShape.Draw(DrawCanvas);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || currentShape == null) return;

            Point end = e.GetPosition(DrawCanvas);
            currentShape.Update(end);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing && currentShape != null)
            {
                isDrawing = false;
                shapes.Add(currentShape);
                currentShape = null;
            }
        }
    }
}
