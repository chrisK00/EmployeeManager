using Dapper;
using EmployeeManager.DataRepository.Developers;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace EmployeeManager.DataRepository.Services
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