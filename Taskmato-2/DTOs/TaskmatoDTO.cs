using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taskmato_2.DTOs
{
    public class TaskmatoDTO
    {
        public int TaskmatoID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} character limit: 100")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "{0} character limit: 200")]
        public string Details { get; set; }
        public int Pomodoros { get; set; }
        public bool Complete { get; set; } = false;
    }
}
