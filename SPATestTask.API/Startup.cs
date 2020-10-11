using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SPATestTask.API.AppStart;
using SPATestTask.Core.Repositories;
using SPATestTask.DAL.Data.DbInitializer;
using SPATestTask.DAL.Repositories;
using SPATestTask.Services.Services;
using SPATestTask.Services.Services.Contracts;

namespace SPATestTask.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddControllersWithViews();
            services.AddSession();

            services.AddMemoryCache();
            services.AddDatabaseContext(Configuration);
            services.AddAutoMapperCustom();
            services.AddScoped<IDbInitializer, DbInitializer>();
      
            return new AutofacServiceProvider(services.ConfigureAutofac());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            app.UseCors("AllowAnyOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbInitializer.Initialize();
        }
    }
}
