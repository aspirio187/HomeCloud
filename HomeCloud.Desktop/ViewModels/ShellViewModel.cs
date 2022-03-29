using HomeCloud.Desktop.Commands;
using HomeCloud.Desktop.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeCloud.Desktop.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly NavigationManager _navigationManager;

        public ICommand NavigateFirstCommand { get; set; }
        public ICommand NavigateSecondCommand { get; set; }

        private ContentControl _currentView;

        public ContentControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                NotifyPropertyChanged();
            }
        }


        private ViewModelBase? _currentViewModel;

        public ViewModelBase? CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSuccess = true;

        public bool IsSuccess
        {
            get { return _isSuccess; }
            set
            {
                _isSuccess = value;
                NotifyPropertyChanged();
            }
        }


        public ShellViewModel(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager ??
                throw new ArgumentNullException(nameof(navigationManager));

            //NavigateFirstCommand = new NavigateCommand<FirstViewModel>(_navigationManager, new FirstViewModel());
            //NavigateSecondCommand = new NavigateCommand<SecondViewModel>(_navigationManager, new SecondViewModel());

            NavigateFirstCommand = new NavigateCommand(NavigateFirst);
            NavigateSecondCommand = new NavigateCommand(NavigateSecond);

            _navigationManager.OnCurrentViewChanged += CurrentViewModelChanged;

            NavigateFirstCommand.Execute(this);
        }

        private void NavigateSecond()
        {
            var currentAssembly = AppDomain.CurrentDomain.GetAssemblies()?.FirstOrDefault(a => a.FullName.Contains(AppDomain.CurrentDomain.FriendlyName));
            var viewType = currentAssembly.DefinedTypes.SingleOrDefault(t => t.Name.Equals("SecondView"));
            CurrentView = (ContentControl)Activator.CreateInstance(viewType);

            //_navigationManager.Navigate("SecondViewModel", true);
        }

        private void NavigateFirst()
        {
            var currentAssembly = AppDomain.CurrentDomain.GetAssemblies()?.FirstOrDefault(a => a.FullName.Contains(AppDomain.CurrentDomain.FriendlyName));
            var viewType = currentAssembly.DefinedTypes.SingleOrDefault(t => t.Name.Equals("FirstView"));
            CurrentView = (ContentControl)Activator.CreateInstance(viewType);
            //_navigationManager.Navigate("FirstViewModel");
        }

        private void CurrentViewModelChanged()
        {
            if (_navigationManager.CurrentView is null) return;
            CurrentViewModel = _navigationManager.CurrentView;
        }
    }
}
