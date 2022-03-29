using HomeCloud.Desktop.Iterators;
using HomeCloud.Desktop.Managers;
using HomeCloud.Desktop.ViewModels;
using HomeCloud.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HomeCloud.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        public void ConfigureServices(ServiceCollection services)
        {
            /****************************************/
            /************** Managers ****************/
            /****************************************/

            services.AddSingleton<NavigationManager>();

            /****************************************/
            /************* ViewModels ***************/
            /****************************************/

            services.AddTransient<ShellViewModel>();
            services.AddTransient<FirstViewModel>();
            services.AddTransient<SecondViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ShellView shellView = new ShellView();
            shellView.Show();
        }
    }
}
