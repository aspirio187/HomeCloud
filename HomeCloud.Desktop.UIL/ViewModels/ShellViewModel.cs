using HomeCloud.Desktop.Commands;
using HomeCloud.Desktop.Managers;
using HomeCloud.FSWatcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly FilesManager _filesManager;
        private readonly Watcher _watcher;

        public ICommand NavigateFirstCommand { get; set; }
        public ICommand NavigateSecondCommand { get; set; }

        private ContentControl? _currentView = null;

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

        public ShellViewModel(NavigationManager navigationManager, FilesManager filesManager)
        {
            _navigationManager = navigationManager ??
                throw new ArgumentNullException(nameof(navigationManager));

            _filesManager = filesManager ??
                throw new ArgumentNullException(nameof(filesManager));

            NavigateFirstCommand = new NavigateCommand(NavigateFirst);
            NavigateSecondCommand = new NavigateCommand(NavigateSecond);

            _navigationManager.OnCurrentViewChanged += CurrentViewModelChanged;

            _watcher = new Watcher(@"C:\Users\soult\Desktop\Dossier à surveiller");
            _watcher.OnErrorOnccured +=  WatcherErrorOccured;
            _watcher.OnChangesOccured +=  WatcherChangesOccured;

            _watcher.Start();

            NavigateFirstCommand.Execute(this);
        }

        private void WatcherChangesOccured(Change change)
        {
            _filesManager.UploadFiles.Add(change);
        }

        private void WatcherErrorOccured(Exception e)
        {
            Debug.WriteLine($"Une erreur est survenue dans le Shell");
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
