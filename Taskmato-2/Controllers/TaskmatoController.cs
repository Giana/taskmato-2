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
        private readonly ITaskmatoService _taskmatoService;
        private readonly ITaskListService _taskListService;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public TaskmatoController(ITaskmatoService taskmatoService, ITaskListService taskListService,
            IUserService userService, UserManager<IdentityUser> userManager)
        {
            _taskmatoService = taskmatoService;
            _taskListService = taskListService;
            _userService = userService;
            _userManager = userManager;
        }

        public ActionResult Index(int taskListId)
        {
            var taskmatos = _taskmatoService.RetrieveTaskmatos(taskListId).ToList();
            
            var dto = new TaskmatoViewModel
            {
                TaskListId = taskListId,
                Taskmatos = taskmatos.Select(x => new TaskmatoDTO
                {
                    TaskmatoId = x.TaskmatoId,
                    Name = x.Name,
                    Details = x.Details,
                    Pomodoros = x.Pomodoros,
                    Complete = x.Complete
                })
                .ToList()
            };

            return View(dto);
        }

        public ActionResult Details(int taskListId, int taskmatoId)
        {
            try
            {
                var taskList = _taskListService.RetrieveTaskList(taskListId);
                var taskmato = taskList.Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Taskmato not found");
                
                var dto = new TaskmatoDTO
                {
                    TaskListId = taskList.TaskListId,
                    TaskmatoId = taskmato.TaskmatoId,
                    Complete = taskmato.Complete,
                    Details = taskmato.Details,
                    Name = taskmato.Name,
                    Pomodoros = taskmato.Pomodoros
                };

                return View(dto);
            }
            catch(Exception e)
            {
                TempData["error"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Create(int taskListId)
        {
            var dto = new TaskmatoDTO { TaskListId = taskListId };

            return View(dto);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(TaskmatoDTO taskmatoDto)
        {
            try
            {
                var user = RetrieveCurrentUser();
                
                var newTaskmato = new Taskmato
                {
                    Name = taskmatoDto.Name,
                    Details = taskmatoDto.Details,
                    Pomodoros = taskmatoDto.Pomodoros,
                    Complete = false
                };

                if(!_taskListService.AddTaskmatoToTaskList(taskmatoDto.TaskListId, newTaskmato))
                {
                    throw new Exception("Taskmato not created");
                }
            }
            catch(Exception e)
            {
                TempData["error"] = e.Message;

                return View(taskmatoDto);
            }  

            return RedirectToAction(nameof(Index), new { taskListId = taskmatoDto.TaskListId } );
        }

        public ActionResult Edit(int taskListId, int taskmatoId)
        {
            var taskList = _taskListService.RetrieveTaskList(taskListId);
            var taskmato = taskList.Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Taskmato not found");

            var dto = new TaskmatoDTO
            {
                TaskmatoId = taskmatoId,
                TaskListId = taskListId,
                Name = taskmato.Name,
                Details = taskmato.Details,
                Pomodoros = taskmato.Pomodoros,
                Complete = taskmato.Complete
            };

            return View(dto);
        }

        [HttpPost]
        public ActionResult EditConfirmed(int taskListId, int taskmatoId, TaskmatoDTO taskmatoDto)
        {
            try
            {
                var taskList = _taskListService.RetrieveTaskList(taskListId);
                var taskmato = taskList.Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Taskmato not found");

                taskmato.Name = taskmatoDto.Name;
                taskmato.Details = taskmatoDto.Details;
                taskmato.Pomodoros = taskmatoDto.Pomodoros;
                taskmato.Details = taskmatoDto.Details;
                taskmato.Complete = taskmatoDto.Complete;

                _taskmatoService.UpdateTaskmato(taskmatoId, taskmato);
            }
            catch(Exception e)
            {
                TempData["error"] = e.Message;
            }

            return RedirectToAction(nameof(Index), new { taskListId = taskListId });
        }

        public ActionResult Delete(int taskListId, int taskmatoId)
        {
            var taskList = _taskListService.RetrieveTaskList(taskListId);
            var taskmato = taskList.Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Taskmato not found");
            
            var taskMatoDto = new TaskmatoDTO
            {
                TaskListId = taskListId,
                TaskmatoId = taskmatoId,
                Name = taskmato.Name,
                Details = taskmato.Details,
                Complete = taskmato.Complete,
                Pomodoros = taskmato.Pomodoros
            };
             
            return View(taskMatoDto);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int taskListId, int taskmatoId)
        { 

            var taskList = _taskListService.RetrieveTaskList(taskListId);
            var taskmato = taskList.Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Taskmato not found");

            _taskmatoService.DeleteTaskmato(taskmatoId);
            
            return RedirectToAction(nameof(Index), new { taskListId = taskListId });
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
