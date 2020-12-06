using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taskmato_2.DTOs
{
    public class RegisterDTO
    {
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage ="Please enter a valid email")]
        public string Email { get; set; }

        [BindProperty]
        [MinLength(4, ErrorMessage = "Username must be a minimum of 4 characters long")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [BindProperty]
        [MinLength(6, ErrorMessage = "Password must be a minimum of 6 characters long")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [BindProperty]
        [MinLength(6, ErrorMessage = "Password must be a minimum of 6 characters long")]
        [Required(ErrorMessage = "You must confirm the password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match")]
        public string PasswordConfirmation { get; set; }
    }
}
