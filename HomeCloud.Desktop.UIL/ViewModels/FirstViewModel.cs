using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Desktop.ViewModels
{
    public class FirstViewModel : ViewModelBase
    {
        private string? _welcomeText;

        public string? WelcomeText
        {
            get { return _welcomeText; }
            set
            {
                _welcomeText = value;
                NotifyPropertyChanged();
            }
        }
    }
}
