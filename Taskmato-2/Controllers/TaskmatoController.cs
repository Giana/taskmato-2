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

namespace Taskmato_2.Controllers
{
    public class TaskmatoController : Controller
    {

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        /*// GET: Task/Details/5
        public ActionResult Details(int? id)
        {
            *//*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Task task = db.Tasks.Find(id);

            if (task == null)
            {
                return HttpNotFound();
            }*//*

            //return View(task);
        }*/

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Taskmato task)
        {
            /*try
            {
                if (ModelState.IsValid)
                {
                    db.Tasks.Add(task);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }*/
            

            return View(task);
        }

        // GET: Task/Edit/5
        /*public ActionResult Edit(int? id)
        {
            *//*if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskToUpdate = db.Tasks.Find(id);

            if(TryUpdateModel(taskToUpdate, "",
               new string[] { "Name", "Details", "Pomodoros", "Complete" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch(DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            }*//*

            //return View(taskToUpdate);
        }*/

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Taskmato task)
        {
            /*if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }*/

            return View(task);
        }

        // GET: Task/Delete/5
        /*public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            *//*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed";
            }

            Task task = db.Tasks.Find(id);

            if (task == null)
            {
                return HttpNotFound();
            }*//*

            //return View(task);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            /*try
            {
                Task task = db.Tasks.Find(id);
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }*/

            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }*/
    }
}
