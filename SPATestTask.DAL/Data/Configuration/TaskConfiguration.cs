using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPATestTask.Core.DataBase;

namespace SPATestTask.DAL.Data.Configuration
{
    class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.TaskName).IsRequired().HasMaxLength(100);

            builder.HasOne(p => p.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(p => p.UserId);
        }
    }
}
