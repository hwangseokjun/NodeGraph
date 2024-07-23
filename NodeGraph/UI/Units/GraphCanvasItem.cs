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
        private Label _label;

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

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_Label") is Label label) {
                _label = label;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            IsSelected = true;
            _ = CaptureMouse();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseCaptured) {
                double ratio = ActualHeight / (_label.ActualHeight / 2);
                Point mousePoint = e.GetPosition(_canvas);
                double left = mousePoint.X - (ActualWidth / 2);
                double top = mousePoint.Y - (ActualHeight / ratio);
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
