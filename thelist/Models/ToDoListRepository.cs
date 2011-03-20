using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace thelist.Models
{ 
    public class ToDoListRepository : IToDoListRepository
    {
        thelistContext context = new thelistContext();

        public IEnumerable<ToDoList> GetAllToDoLists(params Expression<Func<ToDoList, object>>[] includeProperties)
        {
            IQueryable<ToDoList> query = context.ToDoLists;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public ToDoList GetById(int id)
        {
            return context.ToDoLists.Find(id);
        }

        public void InsertOrUpdate(ToDoList todolist)
        {
            if (todolist.Id == default(int)) {
                // New entity
                context.ToDoLists.Add(todolist);
            } else {
                // Existing entity
                context.ToDoLists.Attach(todolist);
                context.Entry(todolist).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var todolist = context.ToDoLists.Find(id);
            context.ToDoLists.Remove(todolist);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IToDoListRepository
    {
		IEnumerable<ToDoList> GetAllToDoLists(params Expression<Func<ToDoList, object>>[] includeProperties);
		ToDoList GetById(int id);
		void InsertOrUpdate(ToDoList todolist);
        void Delete(int id);
        void Save();
    }
}