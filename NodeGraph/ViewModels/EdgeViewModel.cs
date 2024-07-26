using NodeGraph.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeGraph.ViewModels
{
    public class EdgeViewModel : ViewModelBase
    {
        private readonly IInputable _inputable;
        private readonly IOutputable _outputable;

        public Point StartPoint
        {
            get => _outputable.OutputPosition;
            set => OnPropertyChanged(nameof(StartPoint));
        }
        public Point EndPoint
        {
            get => _inputable.InputPosition;
            set => OnPropertyChanged(nameof(EndPoint));
        }

        public EdgeViewModel(IInputable inputable, IOutputable outputable)
        {
            Debug.Assert(inputable != null);
            Debug.Assert(outputable != null);
            _inputable = inputable;
            _outputable = outputable;
            _inputable.InputPositionChanged += _inputable_InputPositionChanged;
            _outputable.OutputPositionChanged += _outputable_OutputPositionChanged;
        }

        private void _outputable_OutputPositionChanged(object sender, EventArgs e)
        {
            StartPoint = new Point(0, 0);
        }

        private void _inputable_InputPositionChanged(object sender, EventArgs e)
        {
            EndPoint = new Point(0, 0);
        }
    }
}
