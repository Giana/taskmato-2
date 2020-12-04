using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public class TaskmatoService
    {
        private TaskmatoContext Context;
        private DbSet<Taskmato> Taskmatos;
        private DbSet<TaskList> TaskLists;

        public TaskmatoService(TaskmatoContext context)
        {
            Context = context;
            Taskmatos = context.Taskmatos;
            TaskLists = context.TaskLists;
        }

        public void AddTaskmato(Taskmato taskmato)
        {

        }

        public void EditTaskmato(int taskmatoId, Taskmato taskmato)
        {

        }

        public void DeleteTaskmato(int taskmatoId)
        {

        }

        public Taskmato GetTaskmato(int taskmatoId)
        {

        }

        public ICollection<Taskmato> RetrieveTaskmatos(int taskListId)
        {

        }


    }
}
