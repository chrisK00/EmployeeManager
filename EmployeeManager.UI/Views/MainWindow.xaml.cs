using EmployeeManager.ViewModels;
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