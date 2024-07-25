using NodeGraph.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.ViewModels
{
    public interface IInputable
    {
        DelegateCommand InputClickCommand { get; }
        event EventHandler InputClicked;
    }

    public interface IOutputable
    {
        DelegateCommand OutputClickCommand { get; }
        event EventHandler OutputClicked;
    }

    public class InputBase : ViewModelBase, IInputable
    {
        public event EventHandler InputClicked;
        public DelegateCommand InputClickCommand { get; private set; }

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
