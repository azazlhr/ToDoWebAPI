using Microsoft.EntityFrameworkCore;
using System;
using ToDoWebAPI.Controllers;
using ToDoWebAPI.Services;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using ToDoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace XUnitTestProject
{
    public class TodoItemsControllerTest
    {
        TodoItemsController TodoItemsController { get; set; }
        ITodoService ITodoService { get; set; }
        ApplicationDbContext DbContext { get; set; }

        public TodoItemsControllerTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TodoItemsDatabase" + Guid.NewGuid()).Options; // used Guid in Db name to avoid sharing same DB across unit tests.
            DbContext = new ApplicationDbContext(options);

            ITodoService = new FakeTodoService(DbContext);

            TodoItemsController = new TodoItemsController(ITodoService);
        }


        [Fact]
        public void GetTodoItems_WhenCalled_ReturnsAll3Items()
        {
            // Arrange
            ITodoService.SeedFakeData();

            // Act            
            dynamic okResultValue = TodoItemsController.GetTodoItems();

            // Assert
            var items = Assert.IsType<List<TodoItem>>(okResultValue.Value);
            Assert.Equal(3, items.Count); // we have seeded 3 fake records so controller must return 3 records
        }

        [Fact]
        public void GetTodoItem_WhenCalled_ReturnsItem()
        {
            // Arrange
            ITodoService.SeedFakeData();

            // Act
            dynamic okResult = TodoItemsController.GetTodoItem(2);

            // Assert
            var item = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal("Task 2", item.Name);
        }

        [Fact]
        public void PostTodoItem_WhenCalled_ReturnsNewlyCreatedRecord()
        {
            // Arrange
            var todoItem = new TodoItem()
            {
                Id = 0,
                Name = "New Task",
                IsComplete = true
            };

            // Act
            dynamic okResult = TodoItemsController.PostTodoItem(todoItem);

            // Assert
            var item = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal("New Task", item.Name); 
        }

        [Fact]
        public void PostTodoItem_WhenCalled_ReturnsDeletedRecord()
        {
            // Arrange
            ITodoService.SeedFakeData();
            
            // Act
            dynamic okResult = TodoItemsController.DeleteTodoItem(1);

            // Assert
            var item = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal("Task 1", item.Name);
        }

        [Fact]
        public void PutTodoItem_WhenCalled_ReturnsUpdatedRecord()
        {
            // Arrange
            ITodoService.SeedFakeData();
            var todoItem = new TodoItem()
            {
                Id = 3,
                Name = "Task 3 Updated",
                IsComplete = false
            };

            // Act
            dynamic okResult = TodoItemsController.PutTodoItem(3, todoItem);

            // Assert
            var item = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal("Task 3 Updated", item.Name);
        }
    }
}
