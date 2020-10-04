using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPATestTask.DAL.Data;

namespace SPATestTask.API.AppStart
{
    public static class DatabaseContextServiceExtension
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DbContextSPATestTask>(options => options.UseSqlite("Data Source=" +
                                                                                     Path.Combine(Directory.GetCurrentDirectory(), "Data\\" + connection)));




        }
    }
}
