using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public interface IUserService
    {
        bool AddUser(User user);
        User RetrieveUserByID(int userId);
        User RetrieveUserByUsername(string username);
        User RetrieveUserByEmail(string email);
        ICollection<User> RetrieveUsers();
        bool IsUsernameAvailable(string username);
    }
}
