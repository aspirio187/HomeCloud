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

        public NavigateCommand(Action navigate)
        {
            Navigate = navigate;
        }

        public event EventHandler? CanExecuteChanged;


        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Navigate?.Invoke();
        }
    }
}
