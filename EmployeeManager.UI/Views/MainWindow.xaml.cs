using EmployeeManager.UI.ViewModels;
using System.Windows;

namespace EmployeeManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
        }
    }
}