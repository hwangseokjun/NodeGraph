using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeGraph.Common
{
    public static class ViewModelLocator
    {
        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, OnAutoWireViewModelChanged));

        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) {
                return;
            }

            if ((bool)e.NewValue && d is FrameworkElement frameworkElement) {
                Type viewType = d.GetType();
                string typeStr = viewType.ToString().Replace("View", "ViewModel");
                Type viewModelType = Type.GetType(typeStr);
                object viewModel = Activator.CreateInstance(viewModelType);
                frameworkElement.DataContext = viewModel;
            }
        }
    }
}
