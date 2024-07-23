using NodeGraph.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title;
        private ContentViewModelBase _currentNode;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public ContentViewModelBase CurrentNode
        {
            get => _currentNode;
            set => SetProperty(ref _currentNode, value);
        }
        public ObservableCollection<ContentViewModelBase> Nodes { get; set; }

        public DelegateCommand TestCommand { get; private set; }
        public DelegateCommand ExportJsonCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }

        public MainWindowViewModel()
        {
            Title = "Node Graph Editor";
            Nodes = new ObservableCollection<ContentViewModelBase>();
            Nodes.CollectionChanged += Nodes_CollectionChanged;
            TestCommand = new DelegateCommand(Test);
            ExportJsonCommand = new DelegateCommand(ExportJson, CanExportJson);
            ClearCommand = new DelegateCommand(Clear, CanClear);
        }

        private void Nodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ExportJsonCommand.RaiseCanExecuteChanged();
            ClearCommand.RaiseCanExecuteChanged();
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

        private void ExportJson()
        {
            throw new NotImplementedException();
        }

        private bool CanExportJson()
        {
            return 0 < Nodes.Count;
        }

        private void Clear()
        {
            Nodes.Clear();
        }

        private bool CanClear()
        {
            return 0 < Nodes.Count;
        }
    }
}
