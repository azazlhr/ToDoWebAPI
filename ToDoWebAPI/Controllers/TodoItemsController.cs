using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebAPI.Models;
using ToDoWebAPI.Services;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace ToDoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService TodoService;

        public TodoItemsController(ITodoService todoService)
        {
            TodoService = todoService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public IActionResult GetTodoItems()
        {
            return Ok(TodoService.GetTodoItems());
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public IActionResult GetTodoItem(long id)
        {
            var todoItem = TodoService.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            } 
            
            var updatedItem = TodoService.Update(todoItem);
            
            return Ok(updatedItem);
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostTodoItem(TodoItem todoItem)
        {
            TodoService.Add(todoItem);
            
            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(long id)
        {
            var todoItem = TodoService.GetTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            TodoService.Delete(id);

            return Ok(todoItem);
        }
    }
}
