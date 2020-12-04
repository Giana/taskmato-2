using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskmato_2.Models;


namespace Taskmato_2.Data.Services
{
    public interface ITaskmatoService
    {
        bool AddTaskmato(Taskmato taskmato);
        bool UpdateTaskmato(int oldTaskmatoId, Taskmato newTaskmato);
        bool DeleteTaskmato(int taskmatoId);
        Taskmato RetrieveTaskmato(int taskmatoId);
        ICollection<Taskmato> RetrieveTaskmatos(int taskListId);
    }
}
