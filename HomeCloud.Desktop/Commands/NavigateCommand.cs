using HomeCloud.Desktop.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeCloud.Desktop.Commands
{
    public class NavigateCommand : ICommand
    {
        public event Action Navigate;
        public bool CanNavigate { get; set; }

        public NavigateCommand(Action navigate, bool canNavigate = true)
        {
            Navigate = navigate;
            CanNavigate = canNavigate;
        }

        public event EventHandler? CanExecuteChanged;


        public bool CanExecute(object? parameter)
        {
            return CanNavigate;
        }

        public void Execute(object? parameter)
        {
            Navigate?.Invoke();
        }
    }
}
