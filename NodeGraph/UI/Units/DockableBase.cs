using NodeGraph.Utils;
using NodeGraph.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraph.UI.Units
{
    public abstract class DockableBase : ContentControl
    {
        private InputDock _inputDock;
        private OutputDock _outputDock;

        static DockableBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockableBase),
                new FrameworkPropertyMetadata(typeof(DockableBase)));
        }

        public override void OnApplyTemplate()
        {
            Canvas canvas = this.FindParentOrNull<Canvas>();
            Debug.Assert(canvas != null);

            if (GetTemplateChild("PART_InputContent") is ContentControl intputContent) {
                _inputDock = DataContext is IInputable ? new InputDock(canvas) : null;
                intputContent.Content = _inputDock;
            }

            if (GetTemplateChild("PART_OutputContent") is ContentControl outputContent) {
                _outputDock = DataContext is IOutputable ? new OutputDock(canvas) : null;
                outputContent.Content = _outputDock;
            }
        }

        public void SetDockPositionInCanvas()
        {
            _inputDock?.SetPositionInCanvas();
            _outputDock?.SetPositionInCanvas();
        }
    }
}
