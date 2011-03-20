using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thelist.Models;

namespace thelist.Controllers
{   
    public class ToDoListsController : Controller
    {
		private readonly IToDoListRepository todolistRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ToDoListsController() : this(new ToDoListRepository())
        {
        }

        public ToDoListsController(IToDoListRepository todolistRepository)
        {
			this.todolistRepository = todolistRepository;
        }

        //
        // GET: /ToDoList/

        public ViewResult Index()
        {
            return View(todolistRepository.GetAllToDoLists());
        }

        //
        // GET: /ToDoList/Details/5

        public ViewResult Details(int id)
        {
            return View(todolistRepository.GetById(id));
        }

        //
        // GET: /ToDoList/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ToDoList/Create

        [HttpPost]
        public ActionResult Create(ToDoList todolist)
        {
            if (ModelState.IsValid) {
                todolistRepository.InsertOrUpdate(todolist);
                todolistRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /ToDoList/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(todolistRepository.GetById(id));
        }

        //
        // POST: /ToDoList/Edit/5

        [HttpPost]
        public ActionResult Edit(ToDoList todolist)
        {
            if (ModelState.IsValid) {
                todolistRepository.InsertOrUpdate(todolist);
                todolistRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /ToDoList/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(todolistRepository.GetById(id));
        }

        //
        // POST: /ToDoList/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            todolistRepository.Delete(id);
            todolistRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

