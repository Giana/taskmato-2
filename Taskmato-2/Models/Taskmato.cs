using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taskmato_2.Models
{
    public class Taskmato
    {
        public int TaskmatoId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Pomodoros { get; set; }
        public bool Complete { get; set; }

        public void IncrementPomodoros()
        {
            // Samantha's code (not currently working)
            /*
            SetPomodoros(GetPomodoros() + 1);
            @{
                var db = Database.Open("Pomodoro");
                var updateQueryString = UPDATE Task SET Pomodoro = GetPromodoros() WHERE Name = GetName();
                db.Execute(updateQueryString, Pomodoro, Name);
            }
            */
        }
    }
}