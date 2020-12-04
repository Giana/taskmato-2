using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public class TaskmatoService : ITaskmatoService
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

        public bool AddTaskmato(Taskmato taskmato)
        {
            Taskmatos.Add(taskmato);

            return Context.SaveChanges() != 0;
        }

        public bool UpdateTaskmato(int oldTaskmatoId, Taskmato newTaskmato)
        {
            Taskmato OldTaskmato = Taskmatos.FirstOrDefault(x => x.TaskmatoId == oldTaskmatoId);
            OldTaskmato.Name = newTaskmato.Name;
            OldTaskmato.Details = newTaskmato.Details;
            OldTaskmato.Pomodoros = newTaskmato.Pomodoros;
            OldTaskmato.Complete = newTaskmato.Complete;

            Taskmatos.Update(OldTaskmato);

            return Context.SaveChanges() != 0;
        }

        public bool DeleteTaskmato(int taskmatoId)
        {
            Taskmato TaskmatoToDelete = Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId);
            Taskmatos.Remove(TaskmatoToDelete);

            return Context.SaveChanges() != 0;
        }

        public Taskmato RetrieveTaskmato(int taskmatoId)
        {
            Taskmato TaskmatoToRetrieve = Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId);

            return TaskmatoToRetrieve;
        }

        public ICollection<Taskmato> RetrieveTaskmatos(int taskListId)
        {
            TaskList TaskList = TaskLists.Include(x => x.Taskmatos).FirstOrDefault(x => x.TaskListId == taskListId);

            return TaskList.Taskmatos;
        }
    }
}
