using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public class TaskListService
    {
        private TaskmatoContext context;
        private DbSet<User> users;
        private DbSet<TaskList> taskLists;

        public TaskListService(TaskmatoContext context)
        {
            this.context = context;
            users = context.TaskmatoUsers;
            taskLists = context.TaskLists;
        }
    }
}
