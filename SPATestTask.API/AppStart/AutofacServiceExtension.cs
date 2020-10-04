using Autofac;
using Microsoft.Extensions.DependencyInjection;
using SPATestTask.DI;

namespace SPATestTask.API.AppStart
{
    public static class AutofacServiceExtension
    {
        public static IContainer ConfigureAutofac(this IServiceCollection services)
        {
            var container = new AutofacContainer();
            return container.Build(services);
        }
    }
}
