using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraph.UI.Units
{
    public class InputDock : DockBase
    {
        static InputDock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputDock),
                new FrameworkPropertyMetadata(typeof(InputDock)));
        }

        public InputDock(Canvas canvas) : base(canvas)
        {
        }
    }
}
