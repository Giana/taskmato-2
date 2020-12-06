using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Taskmato_2.Models;
using Microsoft.AspNetCore.Authorization;
using Taskmato_2.Data.Services;
using Microsoft.AspNetCore.Identity;
using Taskmato_2.DTOs;

namespace Taskmato_2.Controllers
{
    [Authorize]
    public class TaskListController : Controller
    {
        private readonly ITaskmatoService _taskmatoService;
        private readonly ITaskListService _taskListService;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public TaskListController(ITaskmatoService taskmatoService, ITaskListService taskListService,
            IUserService userService, UserManager<IdentityUser> userManager)
        {
            _taskmatoService = taskmatoService;
            _taskListService = taskListService;
            _userService = userService;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var user = RetrieveCurrentUser();

            var taskLists = _taskListService.RetrieveTaskLists(user.UserId).Select(x => new TaskListDTO
            {
                TaskListId = x.TaskListId,
                Date = x.Date
            })
            .ToList();

            return View(taskLists);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(TaskListDTO taskListDto)
        {
            try
            {
                var user = RetrieveCurrentUser();

                var newTaskList = new TaskList
                {
                    Date = taskListDto.Date,
                    User = user
                };

                if(!_taskListService.AddTaskList(newTaskList))
                {
                    throw new Exception("Taskmato not created");
                }
            }
            catch(Exception e)
            {
                TempData["error"] =  e.Message;

                return View(taskListDto);
            }

            return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(int taskListId)
        {
            var user = RetrieveCurrentUser();
            var taskLists = _taskListService.RetrieveTaskLists(user.UserId);
            var taskList = taskLists.FirstOrDefault(x => x.TaskListId == taskListId) ?? throw new Exception("Task list not found");
            
            var taskMatoDto = new TaskListDTO
            {
                TaskListId = taskListId,
                Date = taskList.Date,
                User = user
            };

            return View(taskMatoDto);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int taskListId)
        {
            var user = RetrieveCurrentUser();
            var taskLists = _taskListService.RetrieveTaskLists(user.UserId);
            var taskList = taskLists.FirstOrDefault(x => x.TaskListId == taskListId) ?? throw new Exception("Task list not found");

            _taskListService.DeleteTaskList(taskList.TaskListId);

            return RedirectToAction(nameof(Index));
        }

        public User RetrieveCurrentUser()
        {
            try
            {
                var username = User.Identity.Name;

                if(username != null)
                {
                    var user = _userService.RetrieveUserByUsername(username);

                    return user;
                }

                throw new Exception("Could not retrieve user");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
