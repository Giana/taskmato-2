using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public class UserService : IUserService
    {
        private TaskmatoContext context;
        private DbSet<User> users;

        public UserService(TaskmatoContext context)
        {
            this.context = context;
            users = context.TaskmatoUsers;
        }

        public bool AddUser(User user)
        {
            users.Add(user);

            return context.SaveChanges() != 0;
        }

        public User RetrieveUserById(int userId)
        {
            return users.FirstOrDefault(x => x.UserId == userId) ?? throw new Exception("Could not find user");
        }

        public User RetrieveUserByUsername(string username)
        {
            return users.FirstOrDefault(x => x.Username == username) ?? throw new Exception("Could not find user");
        }

        public User RetrieveUserByEmail(string email)
        {
            return users.FirstOrDefault(x => x.Email == email) ?? throw new Exception("Could not find user");
        }

        public ICollection<User> RetrieveUsers()
        {
            return users.ToList();
        }

        public bool IsUsernameAvailable(string username)
        {
            return users.FirstOrDefault(x => x.Username == username) == null;
        }
    }
}
