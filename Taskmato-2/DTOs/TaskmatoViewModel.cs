﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taskmato_2.DTOs
{
    public class TaskmatoViewModel
    {
        public int TaskListId { get; set; }
        public List<TaskmatoDTO> Taskmatos { get; set; }
    }
}
