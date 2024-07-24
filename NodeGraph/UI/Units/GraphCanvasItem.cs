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

        static GraphCanvasItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphCanvasItem),
                new FrameworkPropertyMetadata(typeof(GraphCanvasItem)));
        }

        public GraphCanvasItem(Canvas canvas)
        {
            Debug.Assert(canvas != null);
            _canvas = canvas;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            _ = CaptureMouse();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseCaptured) {
                Point mousePoint = e.GetPosition(_canvas);
                double left = mousePoint.X - (ActualWidth / 2);
                double top = mousePoint.Y - (ActualHeight / 2);
                Canvas.SetLeft(this, left);
                Canvas.SetTop(this, top);
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
