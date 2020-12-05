﻿using System;
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
            var _User = RetrieveCurrentUser();
            var TaskLists = _taskListService.RetrieveTaskLists(_User.UserId).Select(x => new TaskListDTO
            {
                TaskListID = x.TaskListId,
                Date = x.Date
            })
            .ToList();

            return View(TaskLists);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(TaskListDTO taskListDto)
        {
            if(taskListDto.Date.CompareTo(DateTime.Now) < 0)
            {
                TempData["dateInPastError"] = "The date cannot be in the past";
                return View(taskListDto);
            }

            try
            {
                var _User = RetrieveCurrentUser();
                var NewTaskList = new TaskList
                {
                    Date = taskListDto.Date,
                    User = _User
                };

                if(!_taskListService.AddTaskList(NewTaskList))
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

        [HttpGet]
        public ActionResult Delete(int taskListId)
        {
            var taskList = _taskListService.RetrieveTaskList(taskListId);

            if(taskList == null)
            {
                return NotFound();
            }

            var currDTO = new TaskListDTO { Date = taskList.Date, TaskListID = taskListId };

            return View(currDTO);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int taskListId)
        {
            try
            {
                _taskListService.DeleteTaskList(taskListId);
            }
            catch(Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public User RetrieveCurrentUser()
        {
            try
            {
                var Username = User.Identity.Name;

                if (Username != null)
                {
                    var _User = _userService.RetrieveUserByUsername(Username);

                    return _User;
                }

                throw new Exception("Could not retrieve user");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
