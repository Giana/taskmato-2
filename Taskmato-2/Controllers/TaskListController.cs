using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskmato_2.Data.Services;
using Taskmato_2.Models;

namespace Taskmato_2.Controllers
{
    [Authorize]
    public class TaskListController : Controller
    {
        private ITaskmatoService TaskmatoService;
        private ITaskListService TaskListService;
        private IUserService UserService;
        private UserManager<IdentityUser> UserManager;

        public TaskListController(ITaskmatoService taskmatoService, ITaskListService taskListService,
            IUserService userService, UserManager<IdentityUser> userManager)
        {
            TaskmatoService = taskmatoService;
            TaskListService = taskListService;
            UserService = userService;
            UserManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
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

                if (Username != null)
                {
                    _User = UserService.RetrieveUserByUsername(Username);

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
