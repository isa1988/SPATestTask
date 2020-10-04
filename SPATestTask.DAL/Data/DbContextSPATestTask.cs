using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SPATestTask.Core.DataBase;
using SPATestTask.DAL.Data.Configuration;

namespace SPATestTask.DAL.Data
{
    public class DbContextSPATestTask : DbContext
    {
        public DbContextSPATestTask(DbContextOptions<DbContextSPATestTask> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
