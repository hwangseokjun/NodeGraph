using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraph.UI.Units
{
    public class LabeledTextBox : TextBox
    {
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(LabeledTextBox), new PropertyMetadata(null));

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }
        
        static LabeledTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabeledTextBox),
                new FrameworkPropertyMetadata(typeof(LabeledTextBox)));
        }
    }
}
