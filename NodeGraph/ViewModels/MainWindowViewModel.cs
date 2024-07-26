using NodeGraph.Common;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NodeGraph.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const int _delay = 100;
        private const double _maxZoom = 3.0;
        private const double _minZoom = 0.3;

        private string _title;
        private double _zoomFactor;
        private ViewModelBase _currentElement;
        private IOutputable _currentDock;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public double ZoomFactor
        {
            get => _zoomFactor;
            set => SetProperty(ref _zoomFactor, value);
        }
        public ViewModelBase CurrentElement
        {
            get => _currentElement;
            set => SetProperty(ref _currentElement, value);
        }
        public ObservableCollection<ViewModelBase> Elements { get; private set; }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand RemoveCommand { get; private set; }
        public DelegateCommand ZoomInCommand { get; private set; }
        public DelegateCommand ZoomOutCommand { get; private set; }

        public MainWindowViewModel()
        {
            Elements = new ObservableCollection<ViewModelBase>();
            Elements.CollectionChanged += Elements_CollectionChanged;
            AddCommand = new DelegateCommand(Add);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
            ZoomInCommand = new DelegateCommand(ZoomIn, CanZoomIn);
            ZoomOutCommand = new DelegateCommand(ZoomOut, CanZoomOut);
            Title = "Graph node editor";
            ZoomFactor = 1.0;
        }

        private void Elements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                object newItem = Elements[e.NewStartingIndex];

                if (newItem is IInputable inputable) {
                    inputable.InputClicked += Inputable_InputClicked;
                }

                if (newItem is IOutputable outputable) {
                    outputable.OutputClicked += Outputable_OutputClicked;
                }
            }
        }

        private void Inputable_InputClicked(object sender, EventArgs e)
        {
            if (_currentDock != null) {
                Elements.Add(new EdgeViewModel((IInputable)sender, _currentDock));
            }
        }

        private void Outputable_OutputClicked(object sender, EventArgs e)
        {
            _currentDock = (IOutputable)sender;
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(ZoomFactor)) {
                ZoomInCommand.RaiseCanExecuteChanged();
                ZoomOutCommand.RaiseCanExecuteChanged();
            }

            if (propertyName == nameof(CurrentElement)) {
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        private void Remove()
        {
            if (_currentElement is IInputable inputable) {
                inputable.InputClicked -= Inputable_InputClicked;
                Console.WriteLine("remove input docked");
            }

            if (_currentElement is IOutputable outputable) {
                outputable.OutputClicked -= Outputable_OutputClicked;
                Console.WriteLine("remove output docked");
            }

            if (_currentElement is IDisposable disposable) {
                disposable.Dispose();
            }

            _ = Elements.Remove(_currentElement);
        }

        private bool CanRemove()
        {
            return _currentElement != null;
        }
        private void Add()
        {
            var random = new Random();
            int randi = random.Next(0, 4);

            switch (randi) {
            case 0:
                Elements.Add(new ActionViewModel());
                break;
            case 1:
                Elements.Add(new BranchViewModel());
                break;
            case 2:
                Elements.Add(new ConditionViewModel());
                break;
            case 3:
                Elements.Add(new DialogueViewModel());
                break;
            default:
                Debug.Fail("");
                break;
            }
        }

        private void ZoomIn()
        {
            const double mul = 1.2;
            double zoom = _zoomFactor * mul;
            ZoomFactor = zoom < _maxZoom ? zoom : _maxZoom;
            _ = Task.Delay(_delay);
        }

        private bool CanZoomIn()
        {
            return _zoomFactor < _maxZoom;
        }

        private void ZoomOut()
        {
            const double mul = 0.8;
            double zoom = _zoomFactor * mul;
            ZoomFactor = _minZoom < zoom ? zoom : _minZoom;
            _ = Task.Delay(_delay);
        }

        private bool CanZoomOut()
        {
            return _minZoom < _zoomFactor;
        }
    }
}
