using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taskmato_2.Models;

namespace Taskmato_2.Data
{
    public class TaskmatoInitializer 
    {
        private TaskmatoContext context;
        private UserManager<IdentityUser> userManager;

        public TaskmatoInitializer(TaskmatoContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task InitAsync()
        {
            context.Database.EnsureDeleted();

            if(context.Database.EnsureCreated())
            {
                await InitializeData();
            }
            else
            {
                throw new Exception("Database could not be created");
            }

        }

        private async Task InitializeData()
        {
            const string password = "password";
            await CreateUser("johndoe", "john@doe.whatever", password);

            var userJohn = new User
            {
                Email = "john@doe.whatever",
                Username = "johndoe"
            };

            context.TaskmatoUsers.Add(userJohn);
            InitTasks(userJohn);
        }
        private async Task CreateUser(string username, string email, string password)
        {
            var user = new IdentityUser { UserName = username, Email = email };

            try
            {
                await userManager.CreateAsync(user, password);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        protected void InitTasks(User user)
        {
            var taskList = new TaskList { Date = DateTime.Now };

            var taskmatos = new List<Taskmato>
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

            taskmatos.ForEach(s => taskList.AddTaskmato(s));

            user.TaskLists.Add(taskList);

            context.SaveChanges();
        }
    }
}