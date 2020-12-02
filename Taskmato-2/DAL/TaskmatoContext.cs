using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taskmato.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Taskmato.DAL
{
    public class TaskmatoContext : DbContext
    {
        public TaskmatoContext() : base("TaskmatoContext")
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}