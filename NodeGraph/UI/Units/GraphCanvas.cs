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
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NodeGraph.UI.Units
{
    public class GraphCanvas : ListBox
    {
        private Point _startPoint;
        private Point _currentPoint;
        private double _hOffset;
        private double _vOffset;
        private ScrollViewer _scrollViewer;
        private Canvas _canvas;

        public static readonly DependencyProperty ZoomFactorProperty =
            DependencyProperty.Register(nameof(ZoomFactor), typeof(double), typeof(GraphCanvas), new PropertyMetadata(1.0, OnZoomFactorChanged));
        public static readonly DependencyProperty WheelUpCommandProperty =
            DependencyProperty.Register(nameof(WheelUpCommand), typeof(ICommand), typeof(GraphCanvas), new PropertyMetadata(null));
        public static readonly DependencyProperty WheelDownCommandProperty =
            DependencyProperty.Register(nameof(WheelDownCommand), typeof(ICommand), typeof(GraphCanvas), new PropertyMetadata(null));

        public double ZoomFactor
        {
            get => (double)GetValue(ZoomFactorProperty);
            set => SetValue(ZoomFactorProperty, value);
        }
        public ICommand WheelUpCommand
        {
            get => (ICommand)GetValue(WheelUpCommandProperty);
            set => SetValue(WheelUpCommandProperty, value);
        }
        public ICommand WheelDownCommand
        {
            get => (ICommand)GetValue(WheelDownCommandProperty);
            set => SetValue(WheelDownCommandProperty, value);
        }

        static GraphCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphCanvas),
                new FrameworkPropertyMetadata(typeof(GraphCanvas)));
        }

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_ScrollViewer") is ScrollViewer scrollViewer) {
                _scrollViewer = scrollViewer;
            }

            if (GetTemplateChild("PART_Canvas") is Canvas canvas) {
                _canvas = canvas;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new GraphCanvasItem(_canvas);
        }

        private static void OnZoomFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GraphCanvas graphCanvas) {
                double oldValue = (double)e.OldValue;
                double newValue = (double)e.NewValue;
                var vStoryboard = new Storyboard();
                var hStoryboard = new Storyboard();
                var scale = new ScaleTransform(oldValue, oldValue);
                graphCanvas._canvas.LayoutTransform = scale;

                var anim = new DoubleAnimation();
                var easing = new CubicEase();
                easing.EasingMode = EasingMode.EaseInOut;
                anim.EasingFunction = easing;
                anim.Duration = TimeSpan.FromMilliseconds(300);
                anim.From = oldValue;
                anim.To = newValue;

                hStoryboard.Children.Add(anim);
                Storyboard.SetTargetProperty(anim, new PropertyPath("LayoutTransform.ScaleX"));
                Storyboard.SetTarget(anim, graphCanvas._canvas);
                hStoryboard.Begin();

                vStoryboard.Children.Add(anim);
                Storyboard.SetTargetProperty(anim, new PropertyPath("LayoutTransform.ScaleY"));
                Storyboard.SetTarget(anim, graphCanvas._canvas);
                vStoryboard.Begin();

            }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) {
                return;
            }

            if (0 < e.Delta) {
                WheelUpCommand?.Execute(null);
            } else {
                WheelDownCommand?.Execute(null);
            }

            e.Handled = true;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed) {
                _startPoint = e.GetPosition(_scrollViewer);
                _hOffset = _scrollViewer.HorizontalOffset;
                _vOffset = _scrollViewer.VerticalOffset;
                _ = _scrollViewer.CaptureMouse();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_scrollViewer.IsMouseCaptured) {
                _currentPoint = e.GetPosition(_scrollViewer);
                _scrollViewer.ScrollToHorizontalOffset(_hOffset + (_startPoint.X - _currentPoint.X));
                _scrollViewer.ScrollToVerticalOffset(_vOffset + (_startPoint.Y - _currentPoint.Y));
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (_scrollViewer.IsMouseCaptured) {
                _scrollViewer.ReleaseMouseCapture();
            }
        }
    }
}
