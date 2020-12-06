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
            var CurrDTO = new TaskmatoViewModel
            {
                TaskListId = taskListId,
                Taskmatos = _Taskmatos.Select(x => new TaskmatoDTO
                {
                    TaskmatoId = x.TaskmatoId,
                    Name = x.Name,
                    Details = x.Details,
                    Pomodoros = x.Pomodoros,
                    Complete = x.Complete
                })
                .ToList()
            };

            return View(CurrDTO);
        }

        public ActionResult Details(int taskListId, int taskmatoId)
        {
            try
            {
                var taskList = TaskListService.RetrieveTaskList(taskListId);
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
            catch (Exception e)
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
                var NewTaskmato = new Taskmato
                {
                    Name = taskmatoDto.Name,
                    Details = taskmatoDto.Details,
                    Pomodoros = taskmatoDto.Pomodoros,
                    Complete = false
                };

                if(!TaskListService.AddTaskmatoToTaskListById(taskmatoDto.TaskListId, NewTaskmato))
                {
                    throw new Exception("Taskmato not created");
                }
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return View(taskmatoDto);
            }  

            return RedirectToAction(nameof(Index), new { taskListId = taskmatoDto.TaskListId } );
        }

        public ActionResult Edit(int taskListId, int taskmatoId)
        {
            var taskList = TaskListService.RetrieveTaskList(taskListId);
            var taskmato = taskList.Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Taskmato not found");

            var dto = new TaskmatoDTO {
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
                var taskList = TaskListService.RetrieveTaskList(taskListId);
                var taskmato = taskList.Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Taskmato not found");

                taskmato.Name = taskmatoDto.Name;
                taskmato.Details = taskmatoDto.Details;
                taskmato.Pomodoros = taskmatoDto.Pomodoros;
                taskmato.Details = taskmatoDto.Details;
                taskmato.Complete = taskmatoDto.Complete;

                TaskmatoService.UpdateTaskmato(taskmatoId, taskmato);

                TempData["success"] = "Taskmato updated!";
            }
            catch(Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction(nameof(Index), new { taskListId = taskListId });
        }

        public ActionResult Delete(int taskListId, int taskmatoId)
        {
            var taskList = TaskListService.RetrieveTaskList(taskListId);
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

            var taskList = TaskListService.RetrieveTaskList(taskListId);
            var taskmato = taskList.Taskmatos.FirstOrDefault(x => x.TaskmatoId == taskmatoId) ?? throw new Exception("Taskmato not found");

            TaskmatoService.DeleteTaskmato(taskmatoId);
            
            return RedirectToAction(nameof(Index), new { taskListId = taskListId });
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
