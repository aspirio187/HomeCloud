﻿using HomeCloud.Desktop.Iterators;
using HomeCloud.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HomeCloud.Desktop.Managers
{
    public class NavigationManager
    {
        /// <summary>
        /// The service provider containing all injected services
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// The current assembly
        /// </summary>
        private readonly Assembly _currentAssembly;

        /// <summary>
        /// Event executed when the <see cref="CurrentView"/> value changes
        /// </summary>
        public event Action? OnCurrentViewChanged;

        /// <summary>
        /// The navigation stack containing every view to which we already navigated
        /// </summary>
        public ViewsIterator NavigationStack { get; private set; }

        /// <summary>
        /// The current view, is null by default
        /// </summary>
        private ContentControl? _currentView = null;

        /// <summary>
        /// Get the current view or Set the current view and execute <see cref="OnCurrentViewChanged"/> event
        /// </summary>
        public ContentControl? CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnCurrentViewChanged?.Invoke();
            }
        }

        /// <summary>
        /// Create an instance of NavigationManager
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public NavigationManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ??
                throw new ArgumentNullException(nameof(serviceProvider));

            AppDomain currentDomain = AppDomain.CurrentDomain ?? throw new NullReferenceException(nameof(AppDomain.CurrentDomain));
            Assembly[] assemblies = currentDomain.GetAssemblies();
            _currentAssembly = assemblies.SingleOrDefault(a => a.FullName is not null &&
                                                            a.FullName.Contains(currentDomain.FriendlyName)) ??
                throw new NullReferenceException(nameof(_currentAssembly));

            NavigationStack = new ViewsIterator();
        }

        /// <summary>
        /// Change the currentview to the one given by its name in parameter. Allow to save the view in the
        /// navigation stack
        /// </summary>
        /// <param name="viewName">Desired view's name</param>
        /// <param name="save">Save the view in the navigation stack</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public void Navigate(string viewName, bool save = false)
        {
            if (string.IsNullOrEmpty(viewName)) throw new ArgumentNullException(nameof(viewName));

            ContentControl? view = NavigationStack[viewName];

            if (view is null)
            {
                Type viewType = _currentAssembly.DefinedTypes.SingleOrDefault(t => t.Name.Equals(viewName))
                    ?? throw new NullReferenceException($"No view with name {viewName} was found!");

                view = Activator.CreateInstance(viewType) as ContentControl;
            }

            if (view is null) throw new NullReferenceException(nameof(view));

            CurrentView = view;

            if (save && !NavigationStack.Any(v => v.ToString().Equals(viewName)))
            {
                NavigationStack.Add(view);
                NavigationStack.MoveNext();
            }
        }

        /// <summary>
        /// Check if it is possible to navigate back in the navigation stack
        /// </summary>
        /// <returns>true if </returns>
        public bool CanNavigateBack()
        {
            if (NavigationStack.Length == 0) return false;
            if (NavigationStack.Position == 0) return false;
            return true;
        }

        /// <summary>
        /// Navigate to the previous element in the navigation stack. If <see cref="CanNavigateBack"/> is false, nothing happens.
        /// </summary>
        public void NavigateBack()
        {
            if (CanNavigateBack())
            {
                NavigationStack.MovePrevious();
                CurrentView = NavigationStack.Current;
            }
        }

        /// <summary>
        /// Check if is possible to navigate next in the navigation stack
        /// </summary>
        /// <returns>true If it is possible. false Otherwise</returns>
        public bool CanNavigateNext()
        {
            if (NavigationStack.Length == 0) return false;
            if (NavigationStack.Position == NavigationStack.Length) return false;
            return true;
        }

        /// <summary>
        /// Navigate to the next element in the navigation stack. If <see cref="CanNavigateNext()"/> is false, nothing happens.
        /// </summary>
        public void NavigateNext()
        {
            if (CanNavigateNext())
            {
                NavigationStack.MoveNext();
                CurrentView = NavigationStack.Current;
            }
        }
    }
}
