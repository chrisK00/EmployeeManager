using Dapper;
using EmployeeManager.Models.Developers;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace EmployeeManager.Models
{
    public class DataAccess
    {
        //TODO Make a employeebase class
        public static List<FrontEndDev> LoadEmployees()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                return connection.Query<FrontEndDev>("SELECT * FROM Employee", new DynamicParameters()).ToList();
            }
        }

        private static string LoadConnectionString(string id = "Default") => ConfigurationManager.ConnectionStrings[id].ConnectionString;
    }
}