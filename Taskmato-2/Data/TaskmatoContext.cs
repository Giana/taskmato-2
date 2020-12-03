using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taskmato_2.Models;

namespace Taskmato_2.Data
{
    public class TaskmatoContext : IdentityDbContext
    {
        public DbSet<Taskmato> Taskmatos { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<User> TaskmatoUsers { get; set; }

        public TaskmatoContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Taskmato>()
                .Property(x => x.Name)
                .IsRequired();

            builder.Entity<TaskList>()
                .HasMany(x => x.Taskmatos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskList>()
                .Property(x => x.Date)
                .IsRequired();

            builder.Entity<User>()
                .HasMany(x => x.TaskLists)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}