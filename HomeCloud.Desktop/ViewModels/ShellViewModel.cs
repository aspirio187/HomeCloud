using HomeCloud.Desktop.Commands;
using HomeCloud.Desktop.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeCloud.Desktop.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly NavigationManager _navigationManager;

        public ICommand NavigateFirstCommand { get; set; }
        public ICommand NavigateSecondCommand { get; set; }

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


        public ShellViewModel(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager ??
                throw new ArgumentNullException(nameof(navigationManager));

            //NavigateFirstCommand = new NavigateCommand<FirstViewModel>(_navigationManager, new FirstViewModel());
            //NavigateSecondCommand = new NavigateCommand<SecondViewModel>(_navigationManager, new SecondViewModel());

            NavigateFirstCommand = new NavigateCommand(NavigateFirst);
            NavigateSecondCommand = new NavigateCommand(NavigateSecond);

            _navigationManager.OnCurrentViewModelChanged += CurrentViewModelChanged;

            NavigateFirstCommand.Execute(this);
        }

        private void NavigateSecond()
        {
            _navigationManager.Navigate("SecondViewModel", true);
        }

        private void NavigateFirst()
        {
            _navigationManager.Navigate("FirstViewModel");
        }

        private void CurrentViewModelChanged()
        {
            if (_navigationManager.CurrentViewModel is null) return;
            CurrentViewModel = _navigationManager.CurrentViewModel;
        }
    }
}
