using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public class TaskListService : ITaskListService
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

        public bool AddTaskList(TaskList taskList)
        {
            taskLists.Add(taskList);

            return context.SaveChanges() != 0;
        }

        public bool AddTaskmatoToTaskList(int taskListId, Taskmato taskmato)
        {
            var taskList = taskLists.FirstOrDefault(x => x.TaskListId == taskListId) ?? throw new Exception("Could not find task list");
            
            taskList.AddTaskmato(taskmato);
            taskLists.Update(taskList);

            return context.SaveChanges() != 0;
        }

        public bool DeleteTaskList(int taskListId)
        {
            TaskList taskListToDelete = taskLists.FirstOrDefault(x => x.TaskListId == taskListId) ?? throw new Exception("Could not find task list");

            taskLists.Remove(taskListToDelete);

            return context.SaveChanges() != 0;
        }

        public TaskList RetrieveTaskList(int taskListId)
        {
            TaskList taskListToRetrieve = taskLists.Include(x => x.Taskmatos).FirstOrDefault(x => x.TaskListId == taskListId) ?? throw new Exception("Could not find task list");

            return taskListToRetrieve;
        }

        public ICollection<TaskList> RetrieveTaskLists(int userId)
        {
            User user = users.Include(x => x.TaskLists).ThenInclude(x => x.Taskmatos).FirstOrDefault(x => x.UserId == userId) ?? throw new Exception("Could not find user");

            return user.TaskLists;
        }
    }
}
