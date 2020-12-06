using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public interface ITaskListService
    {
        bool AddTaskList(TaskList taskList);
        bool DeleteTaskList(int taskListId);
        bool AddTaskmatoToTaskListById(int taskListId, Taskmato taskmato);
        TaskList RetrieveTaskList(int taskListId);
        ICollection<TaskList> RetrieveTaskLists(int userId);
    }
}
