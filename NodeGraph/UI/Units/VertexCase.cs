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
    public class VertexCase : ContentControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(VertexCase), new PropertyMetadata(null));
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register(nameof(HeaderBackground), typeof(SolidColorBrush), typeof(VertexCase), new PropertyMetadata(null));

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }
        public SolidColorBrush HeaderBackground
        {
            get => (SolidColorBrush)GetValue(HeaderBackgroundProperty);
            set => SetValue(HeaderBackgroundProperty, value);
        }
                
        static VertexCase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VertexCase),
                new FrameworkPropertyMetadata(typeof(VertexCase)));
        }
    }
}
