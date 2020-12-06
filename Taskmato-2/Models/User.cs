using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Taskmato_2.Models
{
    public class User
    {
        public int UserId { get; set; }
        private string email;

        public string Email
        {
            get => email;

            set
            {
                if(value.Length > 80 || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid email");
                }

                var validEmail = new MailAddress(value);

                if (validEmail.Address != value)
                {
                    throw new FormatException("Invalid email");
                }

                email = value;
            }
        }

        public string Username { get; set; }
        public ICollection<TaskList> TaskLists { get; set; }
        public User()
        {
            TaskLists = new HashSet<TaskList>();
        }
    }
}
