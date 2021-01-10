using EmployeeManager.UI.ViewModels;
using EmployeeManager.UI;
using System.Windows;

namespace EmployeeManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}