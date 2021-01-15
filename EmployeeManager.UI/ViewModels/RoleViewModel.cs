using EmployeeManager.DataRepository.Employees;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace EmployeeManager.UI.ViewModels
{
    public class RoleViewModel : BindableBase
    {
        #region Fields

        private IRole _role;

        #endregion Fields

        #region Delegates

        public event EventHandler RoleRemoved;

        #endregion Delegates

        public IRole Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }

        #region Commands

        public ICommand RemoveRoleCommand { get; }

        #endregion Commands

        public RoleViewModel()
        {
            RemoveRoleCommand = new DelegateCommand(RemoveRole);
        }

        #region Methods

        private void RemoveRole()
        {
            RoleRemoved?.Invoke(this, EventArgs.Empty);
        }

        #endregion Methods
    }
}