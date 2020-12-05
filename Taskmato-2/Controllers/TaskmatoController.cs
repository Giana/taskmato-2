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
    public class TaskmatoController : Controller
    {
        private ITaskmatoService TaskmatoService;
        private ITaskListService TaskListService;
        private IUserService UserService;
        private UserManager<IdentityUser> UserManager;

        public TaskmatoController(ITaskmatoService taskmatoService, ITaskListService taskListService,
            IUserService userService, UserManager<IdentityUser> userManager)
        {
            TaskmatoService = taskmatoService;
            TaskListService = taskListService;
            UserService = userService;
            UserManager = userManager;
        }

        public ActionResult Index(int taskListId)
        {
            var _Taskmatos = TaskmatoService.RetrieveTaskmatos(taskListId).ToList();
            var CurrDTO = new TaskmatoListDTO
            {
                TaskListID = taskListId,
                Taskmatos = _Taskmatos.Select(x => new TaskmatoDTO
                {
                    TaskmatoID = x.TaskmatoId,
                    Name = x.Name,
                    Details = x.Details,
                    Pomodoros = x.Pomodoros,
                    Complete = x.Complete
                })
                .ToList()
            };

            return View(CurrDTO);
        }

        public ActionResult Details(int? id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(int taskListId, TaskmatoDTO taskmatoDto)
        {
            try
            {
                var _TaskList = TaskListService.RetrieveTaskList(taskListId);
                var NewTaskmato = new Taskmato
                {
                    Name = taskmatoDto.Name,
                    Details = taskmatoDto.Details,
                    Pomodoros = taskmatoDto.Pomodoros,
                    Complete = false
                };

                _TaskList.AddTaskmato(NewTaskmato);

                if(!TaskmatoService.AddTaskmato(NewTaskmato))
                {
                    throw new Exception("Taskmato not created");
                }
            }
            catch(Exception e)
            {
                TempData["error"] = e.Message;
                return View(taskmatoDto);
            }

            return RedirectToAction(nameof(Index), taskListId);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new BadRequestResult();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        public User RetrieveCurrentUser()
        {
            try
            {
                var Username = User.Identity.Name;
                
                if(Username != null)
                {
                    var _User = UserService.RetrieveUserByUsername(Username);

                    return _User;
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
