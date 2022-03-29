using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeCloud.Desktop.Locators
{
    public class ViewModelLocator
    {
        public static bool GetAutoConnectedViewModelPRoperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoConnectedViewModelProperty);
        }

        public static void SetAutoConnectedViewModelProperty(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoConnectedViewModelProperty, value);
        }

        public static readonly DependencyProperty AutoConnectedViewModelProperty =
            DependencyProperty.RegisterAttached("AutoConnectedViewModel",
                                                    typeof(bool),
                                                    typeof(ViewModelLocator),
                                                    new PropertyMetadata(false, AutoConnectedViewModelChanged));

        private static void AutoConnectedViewModelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Type viewType = obj.GetType();
            if (viewType is null) throw new NullReferenceException(nameof(viewType));

            string viewTypeName = viewType.FullName ?? throw new NullReferenceException(nameof(viewType.FullName));
            string viewModelTypeName = string.Concat(viewTypeName.Split('.').Last(), "Model");

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var currentAssembly = assemblies.SingleOrDefault(a => a.FullName is not null && a.FullName.Contains(AppDomain.CurrentDomain.FriendlyName));

            if (currentAssembly is null) throw new NullReferenceException(nameof(currentAssembly));
            Type viewModelType = currentAssembly.DefinedTypes.SingleOrDefault(t => t.Name.Equals(viewModelTypeName))
                ?? throw new NullReferenceException(nameof(viewModelTypeName));

            object viewModel = Activator.CreateInstance(viewModelType) ?? throw new NullReferenceException(nameof(viewModel));

            ((FrameworkElement)obj).DataContext = viewModel;
        }
    }
}
