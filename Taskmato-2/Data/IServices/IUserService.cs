using System.Collections.Generic;
using Taskmato_2.Models;

namespace Taskmato_2.Data.Services
{
    public interface IUserService
    {
        bool AddUser(User user);
        User RetrieveUserById(int userId);
        User RetrieveUserByUsername(string username);
        User RetrieveUserByEmail(string email);
        ICollection<User> RetrieveUsers();
        bool IsUsernameAvailable(string username);
    }
}
