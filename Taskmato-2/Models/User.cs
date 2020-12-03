using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Taskmato_2.Models
{
    public class User
    {
        public int UserId { get; set; }
        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (value.Length > 80 || string.IsNullOrEmpty(value))
                    throw new ArgumentException("Email invalid.");

                var emailValid = new MailAddress(value);

                if (emailValid.Address != value)
                    throw new FormatException("Invalid email.");
                _email = value;
            }
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<TaskList> TaskLists { get; set; }
        public User()
        {
            TaskLists = new HashSet<TaskList>();
        }
    }
}
