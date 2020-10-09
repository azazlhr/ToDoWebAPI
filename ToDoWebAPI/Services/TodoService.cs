using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ToDoWebAPI.Models;

namespace ToDoWebAPI.Services
{
    public class TodoService : ITodoService
    {
        protected ApplicationDbContext DbContext = null;
        public TodoService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public void Add(TodoItem todoItem)
        {
            DbContext.TodoItems.Add(todoItem);
            DbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var item = DbContext.TodoItems.SingleOrDefault(x => x.Id == id);
            if (item != null)
                DbContext.TodoItems.Remove(item);
            DbContext.SaveChanges();
        }

        public TodoItem GetTodoItem(long id)
        {
            return DbContext.TodoItems.SingleOrDefault(x => x.Id == id);
        }

        public List<TodoItem> GetTodoItems()
        {
            return DbContext.TodoItems.ToList();
        }
        
        public TodoItem Update(TodoItem todoItem)
        {
            var item = DbContext.TodoItems.SingleOrDefault(x => x.Id == todoItem.Id);
            item.Name = todoItem.Name;
            item.IsComplete = todoItem.IsComplete;
            DbContext.SaveChanges();
            return item;
        }
        virtual public void SeedFakeData()
        {
            throw new NotImplementedException(); // not implemented for DB
        }

    }
}
