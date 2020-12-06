using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public class TaskListService : ITaskListService
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

        public bool AddTaskList(TaskList taskList)
        {
            TaskLists.Add(taskList);

            return Context.SaveChanges() != 0;
        }

        public bool AddTaskmatoToTaskListById(int taskListId, Taskmato taskmato)
        {
            var tasklist = TaskLists.FirstOrDefault(x => x.TaskListId == taskListId) ?? throw new Exception("Couldn't find tasklist");
            tasklist.AddTaskmato(taskmato);
            TaskLists.Update(tasklist);

            return Context.SaveChanges() != 0;
        }

        public bool DeleteTaskList(int taskListId)
        {
            TaskList TaskListToDelete = TaskLists.FirstOrDefault(x => x.TaskListId == taskListId);
            TaskLists.Remove(TaskListToDelete);

            return Context.SaveChanges() != 0;
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
