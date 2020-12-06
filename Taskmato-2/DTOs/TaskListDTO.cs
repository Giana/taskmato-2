using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Taskmato_2.Models;
using Taskmato_2.DTOs.RangeAttributes;

namespace Taskmato_2.DTOs
{
    public class TaskListDTO
    {
        public int TaskListID { get; set; }

        [DataType(DataType.Date)]
        [DateNotInPastAttribute(ErrorMessage = "Your deadline cannot be in the past")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}
