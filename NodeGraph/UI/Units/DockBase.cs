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
    public abstract class DockBase : CheckBox
    {
        private readonly Canvas _canvas;
        private Rectangle _rectangle;
        public static readonly DependencyProperty CheckedBrushProperty =
            DependencyProperty.Register(nameof(CheckedBrush), typeof(Brush), typeof(DockBase), new PropertyMetadata(null));
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(Point), typeof(DockBase), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush CheckedBrush
        {
            get => (Brush)GetValue(CheckedBrushProperty);
            set => SetValue(CheckedBrushProperty, value);
        }
        public Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }
        
        public DockBase(Canvas canvas)
        {
            Debug.Assert(canvas != null);
            _canvas = canvas;
            Loaded += DockBase_Loaded;
        }

        private void DockBase_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= DockBase_Loaded;
            SetPositionInCanvas();
        }

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_Rectangle") is Rectangle rectangle) {
                _rectangle = rectangle;
            }
        }

        public void SetPositionInCanvas()
        {
            Position = _rectangle.TransformToAncestor(_canvas).Transform(new Point(0, 0));
        }
    }
}
