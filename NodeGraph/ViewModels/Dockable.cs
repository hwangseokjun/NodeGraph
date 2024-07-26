using NodeGraph.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeGraph.ViewModels
{
    public interface IInputable
    {
        Point InputPosition { get; set; }
        DelegateCommand InputClickCommand { get; }
        event EventHandler InputClicked;
        event EventHandler InputPositionChanged;
    }

    public interface IOutputable
    {
        Point OutputPosition { get; set; }
        DelegateCommand OutputClickCommand { get; }
        event EventHandler OutputClicked;
        event EventHandler OutputPositionChanged;
    }

    public class InputBase : ViewModelBase, IInputable
    {
        public event EventHandler InputClicked;
        public event EventHandler InputPositionChanged;

        public DelegateCommand InputClickCommand { get; private set; }

        private Point _inputPosition;
        public Point InputPosition
        {
            get => _inputPosition;
            set
            {
                SetProperty(ref _inputPosition, value);
                InputPositionChanged?.Invoke(this, EventArgs.Empty);
            } 
        }
        public InputBase()
        {
            InputClickCommand = new DelegateCommand(OnInputClicked);
        }

        protected virtual void OnInputClicked()
        {
            InputClicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class OutputBase : ViewModelBase, IOutputable
    {
        public event EventHandler OutputClicked;
        public event EventHandler OutputPositionChanged;

        private Point _outputPosition;
        public Point OutputPosition
        {
            get => _outputPosition;
            set
            {
                SetProperty(ref _outputPosition, value);
                OutputPositionChanged?.Invoke(this, EventArgs.Empty);
            } 
        }
        public DelegateCommand OutputClickCommand { get; private set; }

        public OutputBase()
        {
            OutputClickCommand = new DelegateCommand(OnOutputClicked);
        }

        protected virtual void OnOutputClicked()
        {
            OutputClicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class DockableBase : ViewModelBase, IInputable, IOutputable
    {
        private static readonly object _lock;
        private event EventHandler NewInputClicked;
        private event EventHandler NewOutputClicked;
        public event EventHandler InputPositionChanged;
        public event EventHandler OutputPositionChanged;

        private Point _inputPosition;
        public Point InputPosition
        {
            get => _inputPosition;
            set
            {
                SetProperty(ref _inputPosition, value);
                InputPositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private Point _outputPosition;
        public Point OutputPosition
        {
            get => _outputPosition;
            set
            {
                SetProperty(ref _outputPosition, value);
                OutputPositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public DelegateCommand InputClickCommand { get; private set; }
        public DelegateCommand OutputClickCommand { get; private set; }

        static DockableBase()
        {
            _lock = new object();
        }

        public DockableBase()
        {
            InputClickCommand = new DelegateCommand(OnInputClicked);
            OutputClickCommand = new DelegateCommand(OnOutputClicked);
        }

        public event EventHandler InputClicked
        {
            add
            {
                lock (_lock) {
                    NewInputClicked += value;
                }
            }
            remove
            {
                lock (_lock) {
                    NewInputClicked -= value;
                }
            }
        }
        public event EventHandler OutputClicked
        {
            add
            {
                lock (_lock) {
                    NewOutputClicked += value;
                }
            }
            remove
            {
                lock (_lock) {
                    NewOutputClicked -= value;
                }
            }
        }

        protected void OnInputClicked()
        {
            NewInputClicked?.Invoke(this, EventArgs.Empty);
        }

        protected void OnOutputClicked()
        {
            NewOutputClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
