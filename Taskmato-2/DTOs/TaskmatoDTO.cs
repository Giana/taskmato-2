using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Taskmato_2.DTOs
{
    public class TaskmatoDTO
    {
        public int TaskmatoId { get; set; }
        public int TaskListId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "{0} character limit: 100 characters")]
        public string Name { get; set; }
        [BindProperty]
        [StringLength(200, ErrorMessage = "{0} character limit: 200 characters")]
        public string Details { get; set; }
        [BindProperty]
        public int Pomodoros { get; set; }
        [BindProperty]
        public bool Complete { get; set; }
    }
}
