using System;
using System.ComponentModel.DataAnnotations;
using Taskmato_2.Models;

namespace Taskmato_2.DTOs
{
    public class TaskListDTO
    {
        public int TaskListId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}
