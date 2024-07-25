using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraph.UI.Units
{
    public class Branch : ListBox
    {
        static Branch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Branch),
                new FrameworkPropertyMetadata(typeof(Branch)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BranchItem();
        }
    }
}
