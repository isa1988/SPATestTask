using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace SPATestTask.DI
{
    public class AutofacContainer
    {
        public IContainer Build(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .OrderByDescending(a => a.FullName)
                .ToArray();

            ServicesRegister(ref builder, assemblies);
            RepositoriesRegister(ref builder, assemblies);

            return builder.Build();
        }

        private void ServicesRegister(ref ContainerBuilder builder, Assembly[] assemblies)
        {
            var servicesAssembly = assemblies.FirstOrDefault(t => t.FullName.Contains("SPATestTask.Services"));
            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }

        private void RepositoriesRegister(ref ContainerBuilder builder, Assembly[] assemblies)
        {
            var dataAssembly = assemblies.FirstOrDefault(t => t.FullName.Contains("SPATestTask.DAL"));
            builder.RegisterAssemblyTypes(dataAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }


    }
}
