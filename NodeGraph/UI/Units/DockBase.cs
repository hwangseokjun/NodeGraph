using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NodeGraph.UI.Units
{
    public class DockBase : CheckBox
    {
        public static readonly DependencyProperty CheckedBrushProperty =
            DependencyProperty.Register(nameof(CheckedBrush), typeof(DockBase), typeof(DockBase), new PropertyMetadata(null));

        public Brush CheckedBrush
        {
            get => (Brush)GetValue(CheckedBrushProperty);
            set => SetValue(CheckedBrushProperty, value);
        }
    }
}
