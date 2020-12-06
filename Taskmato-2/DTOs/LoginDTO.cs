using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Taskmato_2.DTOs
{
    public class LoginDTO
    {
        [MinLength(4, ErrorMessage = "Username must be a minimum of 4 characters long")]
        [Required(ErrorMessage = "Username is required")]
        [BindProperty]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be a minimum of 6 characters long")]
        [BindProperty]
        public string Password { get; set; }
    }
}
