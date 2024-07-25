using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraph.UI.Units
{
    public class Edge : ContentControl
    {
        static Edge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Edge),
                new FrameworkPropertyMetadata(typeof(Edge)));
        }
    }
}
