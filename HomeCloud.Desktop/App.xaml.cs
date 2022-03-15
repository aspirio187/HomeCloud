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
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        public void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<NavigationManager>();

            services.AddTransient<ShellViewModel>();
            services.AddTransient<FirstViewModel>();
            services.AddTransient<SecondViewModel>();



            services.AddSingleton<ShellView>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ShellView? shellView = _serviceProvider.GetService<ShellView>();
            if (shellView is null) throw new ApplicationException($"The shell view cannot be loaded");
            shellView.DataContext = _serviceProvider.GetService<ShellViewModel>();
            shellView.Show();
        }
    }
}
