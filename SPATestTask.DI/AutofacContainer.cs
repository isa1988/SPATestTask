using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SPATestTask.Core.Repositories;
using SPATestTask.DAL.Repositories;
using SPATestTask.DAL.Unit;
using SPATestTask.DAL.Unit.Contracts;

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

      ServicesRegister(builder, assemblies);
      RepositoriesRegister(builder, assemblies);

      return builder.Build();
    }

    private void ServicesRegister(ContainerBuilder builder, Assembly[] assemblies)
    {
      string line = "SPATestTask.Services".ToLower();
      var servicesAssembly = AssemblyHelper.FindAssemblyByName(assemblies, line);
      builder.RegisterAssemblyTypes(servicesAssembly)
        .Where(t => t.Name.EndsWith("Service"))
        .AsImplementedInterfaces();
    }

    private void RepositoriesRegister(ContainerBuilder builder, Assembly[] assemblies)
    {
      builder.RegisterGeneric(typeof(Repository<>))
        .As(typeof(IRepository<>));
      builder.RegisterGeneric(typeof(Repository<,>))
        .As(typeof(IRepository<,>));

      var dataAssembly = AssemblyHelper.FindAssemblyByName(assemblies, "dal");
      builder.RegisterAssemblyTypes(dataAssembly)
        .Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces();

      builder.RegisterType(typeof(UnitOfWork))
        .As(typeof(IUnitOfWork));
    }
  }
}
