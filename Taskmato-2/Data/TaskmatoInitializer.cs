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
        private readonly TaskmatoContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public TaskmatoInitializer(TaskmatoContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        public async Task InitAsync()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
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
            const string password = "password";
            await CreateUser("gianajinx", "giana.jinx@gmail.com", password);
            await CreateUser("johndoe", "john.doe@gmail.com", password);
            var guser = new User
            {
                Email = "giana.jinx@gmail.com",
                Username = "gianajinx"
            };
            _context.TaskmatoUsers.Add(guser);

            _context.TaskmatoUsers.Add(
                    new User
                    {
                        Email = "john.doe@gmail.com",
                        Username = "johndoe"
                    });

            InitTasks(guser);
        }
        private async Task CreateUser(string username, string email, string password)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            try
            {
                await _userManager.CreateAsync(user, password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        protected void InitTasks(User user)
        {
            var taskList = new TaskList { Date = DateTime.Now };
            var tasks = new List<Taskmato>
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

            tasks.ForEach(s => taskList.AddTaskmato(s));
            user.TaskLists.Add(taskList);
            _context.SaveChanges();
        }
    }
}