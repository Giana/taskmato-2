using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taskmato.Models
{
    public class TaskList
    {
        public int TaskListID { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public void DeleteTask(Task task)
        {
            // Mason's code
        }
    }
}
