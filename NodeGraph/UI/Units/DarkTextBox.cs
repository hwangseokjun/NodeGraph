using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraph.UI.Units
{
    public class DarkTextBox : TextBox
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(DarkTextBox), new PropertyMetadata(null));

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        static DarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DarkTextBox),
                new FrameworkPropertyMetadata(typeof(DarkTextBox)));
        }
    }
}
