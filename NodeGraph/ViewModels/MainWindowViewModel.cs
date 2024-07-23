using NodeGraph.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ContentViewModelBase _currentNode;

        public ContentViewModelBase CurrentNode
        {
            get => _currentNode;
            set => SetProperty(ref _currentNode, value);
        }

        public ObservableCollection<ContentViewModelBase> Nodes { get; set; }

        public DelegateCommand TestCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }

        public MainWindowViewModel()
        {
            Nodes = new ObservableCollection<ContentViewModelBase>();
            TestCommand = new DelegateCommand(Test);
            ClearCommand = new DelegateCommand(Clear, CanClear);
        }

        private void Test()
        {
            Random random = new Random();
            int i = random.Next(0, 2);
            switch (i) {
            case 0:
                Nodes.Add(new BranchContentViewModel());
                break;
            case 1:
                Nodes.Add(new DialogueContentViewModel());
                break;
            case 2:
                Nodes.Add(new ConditionContentViewModel());
                break;
            default:
                Debug.Fail("");
                break;
            }
            
        }

        private void Clear()
        {
            Nodes.Clear();
        }

        private bool CanClear()
        {
            return true;
        }
    }
}
