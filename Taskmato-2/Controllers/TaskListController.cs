using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taskmato_2.Controllers
{
    public class TaskListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
