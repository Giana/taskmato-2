﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taskmato_2.Models
{
    public class TaskList
    {
        public int TaskListId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Taskmato> Taskmatos { get; set; }
        public User User { get; set; }

        public TaskList()
        {
            Taskmatos = new HashSet<Taskmato>();
        }
        public TaskList(DateTime date) : this()
        {
            Date = date;
        }

        public void AddTaskmato(Taskmato taskmato)
        {
            Taskmatos.Add(taskmato);
        }

        public void DeleteTaskmato(Taskmato taskmato)
        {
            // Mason's code
        }
    }
}
