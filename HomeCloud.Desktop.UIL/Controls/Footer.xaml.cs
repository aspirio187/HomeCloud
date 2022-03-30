using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeCloud.Desktop.Controls
{
    /// <summary>
    /// Logique d'interaction pour Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(nameof(Status), typeof(bool), typeof(Footer));
        public bool Status { get; set; }

        public Footer()
        {
            InitializeComponent();
        }

        private void Footer_Loaded(object sender, RoutedEventArgs e)
        {
            if (Status == true)
            {
                tblStatus.Text = "Connected";
            }
            else
            {
                tblStatus.Text = "Disconnected";
            }
        }
    }
}
