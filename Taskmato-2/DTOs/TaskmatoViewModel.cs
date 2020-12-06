using System.Collections.Generic;

namespace Taskmato_2.DTOs
{
    public class TaskmatoViewModel
    {
        public int TaskListId { get; set; }
        public List<TaskmatoDTO> Taskmatos { get; set; }
    }
}
