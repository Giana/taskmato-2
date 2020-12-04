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
        private TaskmatoContext Context;
        private DbSet<User> Users;

        public UserService(TaskmatoContext context)
        {
            Context = context;
            Users = context.TaskmatoUsers;
        }
        public bool AddUser(User user)
        {
            Users.Add(user);

            return Context.SaveChanges() != 0;
        }

        public User RetrieveUserByID(int userId)
        {
            return Users.FirstOrDefault(x => x.UserId == userId);
        }

        public User RetrieveUserByUsername(string username)
        {
            return Users.FirstOrDefault(x => x.Username == username);
        }

        public User RetrieveUserByEmail(string email)
        {
            return Users.FirstOrDefault(x => x.Email == email);
        }

        public ICollection<User> RetrieveUsers()
        {
            return Users.ToList();
        }

        public bool IsUsernameAvailable(string username)
        {
            return Users.FirstOrDefault(x => x.Username == username) == null;
        }
    }
}
