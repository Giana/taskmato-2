using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Web;
//using System.Web.Mvc;
using Taskmato_2.Models;
using Microsoft.AspNetCore.Authorization;
using Taskmato_2.Data.Services;
using Microsoft.AspNetCore.Identity;

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
            return View();
        }

        public ActionResult Details(int? id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int? id)
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
            return RedirectToAction("Index");
        }

        public User RetrieveCurrentUser()
        {
            User _User;
            try
            {
                string Username = User.Identity.Name;
                
                if(Username != null)
                {
                    _User = UserService.RetrieveUserByUsername(Username);

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
