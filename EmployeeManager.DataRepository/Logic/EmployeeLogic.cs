using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace EmployeeManager.Models
{
    public static class EmployeeLogic
    {
        // string CreateMail(); switch on employee type
        //decimal calcsalary switch on employee type

        public static void SetCultureToUS() => Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
    }
}
