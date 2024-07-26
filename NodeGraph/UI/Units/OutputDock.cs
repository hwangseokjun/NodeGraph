using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraph.UI.Units
{
    public class OutputDock : DockBase
    {
        static OutputDock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OutputDock),
                new FrameworkPropertyMetadata(typeof(OutputDock)));
        }

        public OutputDock(Canvas canvas) : base(canvas)
        {
        }
    }
}
