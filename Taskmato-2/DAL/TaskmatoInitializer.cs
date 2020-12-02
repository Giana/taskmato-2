using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Taskmato.Models;

namespace Taskmato.DAL
{
    public class TaskmatoInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TaskmatoContext>
    {
        protected override void Seed(TaskmatoContext context)
        {
            var tasks = new List<Task>
            {
            new Task{Name="Walk the dog",Details="Twice around the block",Pomodoros=0,Complete=false},
            new Task{Name="Mow the lawn",Details="Mower in the shed",Pomodoros=0,Complete=false},
            new Task{Name="Call Dad",Details="",Pomodoros=0,Complete=false},
            new Task{Name="Do math homework",Details="Questions 4-8 on worksheet",Pomodoros=0,Complete=false},
            new Task{Name="Wash the sheets",Details="",Pomodoros=0,Complete=false},
            new Task{Name="Chug a gallon of water",Details="",Pomodoros=0,Complete=false},
            new Task{Name="Clean the litter box",Details="",Pomodoros=0,Complete=false},
            new Task{Name="Find birth certificate",Details="In the depths of the basement?",Pomodoros=0,Complete=false},
            new Task{Name="Text Dad",Details="",Pomodoros=0,Complete=false}
            };

            tasks.ForEach(s => context.Tasks.Add(s));
            context.SaveChanges();

            var taskLists = new List<TaskList>
            {
            new TaskList{Date=DateTime.Parse("2020-11-10")},
            new TaskList{Date=DateTime.Parse("2020-11-11")},
            new TaskList{Date=DateTime.Parse("2020-11-12")},
            };

            taskLists.ForEach(s => context.TaskLists.Add(s));
            context.SaveChanges();
        }
    }
}