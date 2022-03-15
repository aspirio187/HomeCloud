using HomeCloud.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Desktop.Managers
{
    public class NavigationManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Assembly _currentAssembly;

        public event Action? OnCurrentViewModelChanged;

        public List<ViewModelBase> NavigationStack { get; private set; }

        private ViewModelBase? _currentViewModel;

        public ViewModelBase? CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged();
            }
        }


        public NavigationManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ??
                throw new ArgumentNullException(nameof(serviceProvider));

            _currentAssembly = AppDomain.CurrentDomain.GetAssemblies()?.FirstOrDefault(a => a.FullName.Contains(AppDomain.CurrentDomain.FriendlyName));
            if (_currentAssembly is null) throw new NullReferenceException($"Could not load assembly with name {nameof(_currentAssembly)}!");

            NavigationStack = new List<ViewModelBase>();
        }

        public void CurrentViewModelChanged()
        {
            OnCurrentViewModelChanged?.Invoke();
        }

        public void Navigate(string viewModelName, bool save = false)
        {
            if (string.IsNullOrEmpty(viewModelName)) throw new ArgumentNullException(nameof(viewModelName));

            ViewModelBase? vm = NavigationStack.SingleOrDefault(v => v.ToString().Equals(viewModelName));

            if (vm is null)
            {
                Type? vmType = _currentAssembly.DefinedTypes.SingleOrDefault(t => t.Name.Equals(viewModelName));
                if (vmType is null) throw new NullReferenceException($"No ViewModel with name {nameof(viewModelName)} was found!");
                vm = _serviceProvider.GetService(vmType) as ViewModelBase;
            }

            CurrentViewModel = vm;

            if (save && !NavigationStack.Any(v => v.ToString().Equals(viewModelName)))
            {
                NavigationStack.Add(vm);
            }
        }

        public bool CanNavigateBack()
        {
            if (NavigationStack.Count == 0) return false;
            //if(NavigationStack.las)
            return true;
        }
    }
}
