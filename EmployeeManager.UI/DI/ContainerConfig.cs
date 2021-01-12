using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EmployeeManager.DataRepository.Services;

namespace EmployeeManager.UI.DI
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AppDbContext>().As<AppDbContext>();
            return builder.Build();
        }
    }
}
