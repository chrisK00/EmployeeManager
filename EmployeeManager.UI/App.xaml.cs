using EmployeeManager.DataRepository.Services;
using EmployeeManager.UI.ViewModels;
using EmployeeManager.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Windows;

namespace EmployeeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();
            //add the dependencies
            services.AddDbContext<AppDbContext>();           
            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowViewModel>();

            var provider = services.BuildServiceProvider();
            var mainWindow = provider.GetRequiredService<MainWindow>();
            //replaces app.xaml startup
            mainWindow.Show();
        }
    }
}