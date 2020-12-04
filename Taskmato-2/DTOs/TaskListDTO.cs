using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Taskmato_2.DTOs
{
    public class TaskListDTO
    {
        public int TaskListID { get; set; }
        [DataType(DataType.Date)]
        [BindProperty, DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
