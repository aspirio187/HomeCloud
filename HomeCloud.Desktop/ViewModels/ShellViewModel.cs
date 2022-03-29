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

        private ContentControl _currentView = null;

        public ContentControl? CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value ?? throw new NullReferenceException();
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

            NavigateFirstCommand = new NavigateCommand(NavigateFirst);
            NavigateSecondCommand = new NavigateCommand(NavigateSecond);

            _navigationManager.OnCurrentViewChanged += CurrentViewModelChanged;

            NavigateFirstCommand.Execute(this);
        }

        private void NavigateSecond()
        {
            _navigationManager.Navigate("SecondView", true);
        }

        private void NavigateFirst()
        {
            _navigationManager.Navigate("FirstView");
        }

        private void CurrentViewModelChanged()
        {
            if (_navigationManager.CurrentView is null) return;
            CurrentView = _navigationManager.CurrentView;
        }
    }
}
