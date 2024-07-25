using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraph.UI.Units
{
    public class BranchItem : DockableBase
    {
        static BranchItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BranchItem),
                new FrameworkPropertyMetadata(typeof(BranchItem)));
        }
    }
}
