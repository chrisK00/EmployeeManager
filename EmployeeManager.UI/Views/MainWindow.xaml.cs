using EmployeeManager.UI.ViewModels;
using EmployeeManager.UI;
using System.Windows;
using EmployeeManager.UI.DI;
using Autofac;
using EmployeeManager.DataRepository.Services;

namespace EmployeeManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var container = ContainerConfig.Configure();
            var start = container.Resolve<AppDbContext>();
            DataContext = new MainWindowViewModel(start);
        }
    }
}