using NodeGraph.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NodeGraph.UI.Units
{
    public class GraphCanvasItem : ListBoxItem
    {
        private readonly Canvas _canvas;
        private DockableBase _dockable;
        private Point _clickPoint;
        private Point _startPoint;

        static GraphCanvasItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphCanvasItem),
                new FrameworkPropertyMetadata(typeof(GraphCanvasItem)));
        }

        public GraphCanvasItem(Canvas canvas)
        {
            Debug.Assert(canvas != null);
            _canvas = canvas;
            Loaded += GraphCanvasItem_Loaded;
        }

        private void GraphCanvasItem_Loaded(object sender, RoutedEventArgs e)
        {
            _dockable = this.FindChildOrNull<DockableBase>();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            _clickPoint = Mouse.GetPosition(_canvas);
            _startPoint = TransformToAncestor(_canvas).Transform(new Point(0, 0));
            _ = CaptureMouse();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseCaptured) {
                Point mousePoint = Mouse.GetPosition(_canvas);
                double left = _startPoint.X + (mousePoint.X - _clickPoint.X);
                double top = _startPoint.Y + (mousePoint.Y - _clickPoint.Y);
                Canvas.SetLeft(this, left);
                Canvas.SetTop(this, top);
                _dockable?.SetDockPositionInCanvas();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured) {
                ReleaseMouseCapture();
            }
        }
    }
}
