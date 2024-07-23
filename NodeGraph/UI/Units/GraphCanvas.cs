using NodeGraph.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NodeGraph.UI.Units
{
    public class GraphCanvas : ListBox
    {
        private Canvas _canvas;

        static GraphCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphCanvas),
                new FrameworkPropertyMetadata(typeof(GraphCanvas)));
        }

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_Canvas") is Canvas canvas) {
                _canvas = canvas;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new GraphCanvasItem(_canvas);
        }
    }
}
