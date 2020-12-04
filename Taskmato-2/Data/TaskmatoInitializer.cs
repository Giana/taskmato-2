using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Taskmato_2.Models;

namespace Taskmato_2.Data
{
    public class TaskmatoInitializer 
    {
        private TaskmatoContext Context;
        private UserManager<IdentityUser> UserManager;

        public TaskmatoInitializer(TaskmatoContext context, UserManager<IdentityUser> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }

        public async Task InitAsync()
        {
            Context.Database.EnsureDeleted();

            if (Context.Database.EnsureCreated())
            {
                await InitializeData();

            }
            else
            {
                throw new Exception("Database Unipack could not be created");
            }

        }

        private async Task InitializeData()
        {
            const string Password = "password";
            await CreateUser("gianajinx", "giana.jinx@gmail.com", Password);
            await CreateUser("johndoe", "john.doe@gmail.com", Password);

            var userGiana = new User
            {
                Email = "giana@giana.dev",
                Username = "giana"
            };

            Context.TaskmatoUsers.Add(userGiana);
            InitTasks(userGiana);
        }
        private async Task CreateUser(string username, string email, string password)
        {
            var User = new IdentityUser { UserName = username, Email = email };

            try
            {
                await UserManager.CreateAsync(User, password);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        protected void InitTasks(User user)
        {
            var TaskList = new TaskList { Date = DateTime.Now };
            var Tasks = new List<Taskmato>
            {
            new Models.Taskmato{ Name="Walk the dog", Details="Twice around the block", Pomodoros=0, Complete=false},
            new Models.Taskmato{ Name="Mow the lawn", Details="Mower in the shed", Pomodoros=0, Complete=false},
            new Models.Taskmato{ Name="Call Dad", Details="", Pomodoros=0, Complete=false},
            new Models.Taskmato{ Name="Do math homework", Details="Questions 4-8 on worksheet", Pomodoros=0, Complete=false},
            new Models.Taskmato{ Name="Wash the sheets", Details="", Pomodoros=0, Complete=false},
            new Models.Taskmato{ Name="Chug a gallon of water", Details="", Pomodoros=0, Complete=false},
            new Models.Taskmato{ Name="Clean the litter box", Details="", Pomodoros=0, Complete=false},
            new Models.Taskmato{ Name="Find birth certificate", Details="In the depths of the basement?", Pomodoros=0, Complete=false},
            new Models.Taskmato{ Name="Text Dad", Details="", Pomodoros=0, Complete=false}
            };

            Tasks.ForEach(s => TaskList.AddTaskmato(s));
            user.TaskLists.Add(TaskList);
            Context.SaveChanges();
        }
    }
}