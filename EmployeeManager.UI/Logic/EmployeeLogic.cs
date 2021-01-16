using System.Globalization;
using System.Threading;

namespace EmployeeManager.DataRepository.Logic
{
    public static class EmployeeLogic
    {
        // string CreateMail(); switch on employee type
        //decimal calcsalary switch on employee type
        public static string CreateMail(UI.ViewModels.IEmployeeViewModel empVM)
        {
            if (empVM.SelectedRole is null || empVM.SelectedRole.Name.Length < 3)
            {
                return "Na";
            }

            var roleName = empVM.SelectedRole.Name;
            var firstCharsOfSelectedRole = roleName.Substring(0, 3);
            string secondCharsOfSelectedRole = null;
            if (roleName.Contains(" "))
            {
                secondCharsOfSelectedRole = roleName.Substring(roleName.IndexOf(" ") + 1);
            }

            return secondCharsOfSelectedRole != null ? $"{empVM.Employee.Name}.{firstCharsOfSelectedRole}{secondCharsOfSelectedRole}@mail.com" :
                $"{empVM.Employee.Name}.{firstCharsOfSelectedRole}@mail.com";
        }

        public static void SetCultureToUS() => Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
    }
}