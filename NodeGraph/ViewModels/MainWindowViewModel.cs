using NodeGraph.Common;
using System;
using System.Collections.ObjectModel;
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
        private GraphViewModelBase _currentElement;

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
        public GraphViewModelBase CurrentElement
        {
            get => _currentElement;
            set => SetProperty(ref _currentElement, value);
        }
        public ObservableCollection<GraphViewModelBase> Elements { get; private set; }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand ZoomInCommand { get; private set; }
        public DelegateCommand ZoomOutCommand { get; private set; }

        public MainWindowViewModel()
        {
            Elements = new ObservableCollection<GraphViewModelBase>();
            AddCommand = new DelegateCommand(Add);
            ZoomInCommand = new DelegateCommand(ZoomIn, CanZoomIn);
            ZoomOutCommand = new DelegateCommand(ZoomOut, CanZoomOut);
            Title = "Graph node editor";
            ZoomFactor = 1.0;
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(ZoomFactor)) {
                ZoomInCommand.RaiseCanExecuteChanged();
                ZoomOutCommand.RaiseCanExecuteChanged();
            }
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
