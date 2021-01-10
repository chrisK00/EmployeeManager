using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.UI.ViewModels
{
    public interface IEmployeeViewModel
    {
        event EventHandler EmployeeFired;
    }
}
