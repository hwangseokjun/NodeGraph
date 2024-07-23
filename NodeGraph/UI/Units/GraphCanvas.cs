using NodeGraph.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NodeGraph.UI.Units
{
    public class GraphCanvas : ListBox
    {
        private Point _startPoint;
        private double hOffset;
        private double vOffset;
        private ScrollViewer _scrollViewer;
        private Canvas _canvas;

        static GraphCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphCanvas),
                new FrameworkPropertyMetadata(typeof(GraphCanvas)));
        }

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_Canvas") is Canvas canvas) {
                _canvas = canvas;
            }

            if (GetTemplateChild("PART_ScrollViewer") is ScrollViewer scrollViewer) {
                _scrollViewer = scrollViewer;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new GraphCanvasItem(_canvas);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Canvas) {
                _startPoint = e.GetPosition(_scrollViewer);
                _ = CaptureMouse();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseCaptured) {
                
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
