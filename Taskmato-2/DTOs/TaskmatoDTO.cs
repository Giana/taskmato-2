using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taskmato_2.DTOs
{
    public class TaskmatoDTO
    {
        public int TaskmatoId { get; set; }
        public int TaskListId { get; set; }
        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "{0} character limit: 100")]
        public string Name { get; set; }
        [BindProperty]
        [StringLength(200, ErrorMessage = "{0} character limit: 200")]
        public string Details { get; set; }
        [BindProperty]
        public int Pomodoros { get; set; }
        [BindProperty]
        public bool Complete { get; set; }
    }
}
