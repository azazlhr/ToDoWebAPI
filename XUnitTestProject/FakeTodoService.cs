using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ToDoWebAPI.Models;

namespace ToDoWebAPI.Services
{
    public class FakeTodoService : TodoService
    {
        public FakeTodoService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
        public override void SeedFakeData()
        {
            DbContext.TodoItems.Add(new TodoItem()
            {
                Id = 1,
                Name = "Task 1",
                IsComplete = true
            });

            DbContext.TodoItems.Add(new TodoItem()
            {
                Id = 2,
                Name = "Task 2",
                IsComplete = true
            });

            DbContext.TodoItems.Add(new TodoItem()
            {
                Id = 3,
                Name = "Task 3",
                IsComplete = true
            });
            DbContext.SaveChanges();
        }
    }
}
