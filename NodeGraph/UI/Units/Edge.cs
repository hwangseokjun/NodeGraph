using NodeGraph.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NodeGraph.UI.Units
{
    public class Edge : ContentControl
    {
        private readonly Line _line;
        private Point _startPoint;
        private GraphCanvasItem _canvasItem;

        public static readonly DependencyProperty StartPointProperty =
            DependencyProperty.Register(nameof(StartPoint), typeof(Point), typeof(Edge), new PropertyMetadata(OnStartPointChanged));
        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register(nameof(EndPoint), typeof(Point), typeof(Edge), new PropertyMetadata(OnEndPointChanged));

        public Point StartPoint
        {
            get => (Point)GetValue(StartPointProperty);
            set => SetValue(StartPointProperty, value);
        }
        public Point EndPoint
        {
            get => (Point)GetValue(EndPointProperty);
            set => SetValue(EndPointProperty, value);
        }
        
        public Edge()
        {
            _line = new Line();
            _line.IsHitTestVisible = false;
            _line.Stroke = Brushes.White;
            _line.StrokeThickness = 2.0;
            Content = _line;
        }

        public void DrawEdge()
        {
            _canvasItem = _canvasItem ?? this.FindParentOrNull<GraphCanvasItem>();
            Debug.Assert(_canvasItem != null);
            Canvas.SetLeft(_canvasItem, StartPoint.X);
            Canvas.SetTop(_canvasItem, StartPoint.Y);
            _line.X1 = 0;
            _line.Y1 = 0;
            _line.X2 = EndPoint.X - StartPoint.X;
            _line.Y2 = EndPoint.Y - StartPoint.Y;
        }

        private static void OnStartPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Edge edge) {
                edge.DrawEdge();
            }
        }

        private static void OnEndPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Edge edge) {
                edge.DrawEdge();
            }
        }
    }
}
