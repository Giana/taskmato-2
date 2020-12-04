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
        private TaskmatoContext Context;
        private DbSet<User> Users;
        private DbSet<TaskList> TaskLists;

        public TaskListService(TaskmatoContext context)
        {
            Context = context;
            Users = context.TaskmatoUsers;
            TaskLists = context.TaskLists;
        }

        public void AddTaskList(TaskList taskList)
        {
            TaskLists.Add(taskList);
        }

        public void DeleteTaskList(int taskListId)
        {
            TaskList TaskListToDelete = TaskLists.FirstOrDefault(x => x.TaskListId == taskListId);
            TaskLists.Remove(TaskListToDelete);
        }

        public TaskList RetrieveTaskList(int taskListId)
        {
            TaskList TaskListToRetrieve = TaskLists.Include(x => x.Taskmatos).FirstOrDefault(x => x.TaskListId == taskListId);

            return TaskListToRetrieve;
        }

        public ICollection<TaskList> RetrieveTaskLists(int userId)
        {
            User User = Users.Include(x => x.TaskLists).ThenInclude(x => x.Taskmatos).FirstOrDefault(x => x.UserId == userId);

            return User.TaskLists;
        }
    }
}
