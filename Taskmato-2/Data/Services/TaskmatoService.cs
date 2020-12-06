using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public class TaskmatoService : ITaskmatoService
    {
        private TaskmatoContext context;
        private DbSet<Taskmato> taskmatos;
        private DbSet<TaskList> taskLists;

        public TaskmatoService(TaskmatoContext context)
        {
            this.context = context;
            taskmatos = context.Taskmatos;
            taskLists = context.TaskLists;
        }

        public bool AddTaskmato(Taskmato taskmato)
        {
            taskmatos.Add(taskmato);

            return context.SaveChanges() != 0;
        }

        public bool UpdateTaskmato(int oldTaskmatoId, Taskmato newTaskmato)
        {
            Taskmato oldTaskmato = taskmatos.FirstOrDefault(x => x.TaskmatoId == oldTaskmatoId) ?? throw new Exception("Could not find task");
            oldTaskmato.Name = newTaskmato.Name;
            oldTaskmato.Details = newTaskmato.Details;
            oldTaskmato.Pomodoros = newTaskmato.Pomodoros;
            oldTaskmato.Complete = newTaskmato.Complete;

            taskmatos.Update(oldTaskmato);

            return context.SaveChanges() != 0;
        }

        public bool DeleteTaskmato(int taskmatoId)
        {
            Taskmato taskmatoToDelete = taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Could not find task");

            taskmatos.Remove(taskmatoToDelete);

            return context.SaveChanges() != 0;
        }

        public Taskmato RetrieveTaskmato(int taskmatoId)
        {
            Taskmato taskmatoToRetrieve = taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Could not find task");

            return taskmatoToRetrieve;
        }

        public ICollection<Taskmato> RetrieveTaskmatos(int taskListId)
        {
            TaskList taskList = taskLists.Include(x => x.Taskmatos).FirstOrDefault(x => x.TaskListId == taskListId) ?? throw new Exception("Could not find task list");

            return taskList.Taskmatos;
        }
    }
}
