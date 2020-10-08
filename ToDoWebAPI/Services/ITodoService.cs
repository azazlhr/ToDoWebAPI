using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebAPI.Models;

namespace ToDoWebAPI.Services
{
    public interface ITodoService
    {
        List<TodoItem> GetTodoItems();
        TodoItem GetTodoItem(long id);
        void Add(TodoItem todoItem);
        void Update(TodoItem todoItem);
        void Delete(long id);
    }
}
