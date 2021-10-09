using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kritner.TodoBackend.Core.ApiModels;
using Kritner.TodoBackend.Core.ExtensionMethods;
using Kritner.TodoBackend.Core.Models;
using Kritner.TodoBackend.Core.Providers;
using Kritner.TodoBackend.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kritner.TodoBackend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IIdentifierProvider _identifierProvider;

        public TodoController(ITodoRepository todoRepository, IIdentifierProvider identifierProvider)
        {
            _todoRepository = todoRepository;
            _identifierProvider = identifierProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TodoGet>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoRepository.Get();
            var rootUrl = GetRootUrl();
            
            return Ok(todos.ToList().Select(s => s.ToTodoGet(rootUrl)));
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(TodoGet), 200)]
        public async Task<IActionResult> Create(TodoCreate todoCreate)
        {
            var todo = new TodoItem()
            {
                Id = _identifierProvider.GetId(),
                Title = todoCreate.Title,
                Completed = todoCreate.Completed,
                Order = todoCreate.Order,
            };
            
            await _todoRepository.Create(todo);

            var createdTodo = todo.ToTodoGet(GetRootUrl());
            return Created(createdTodo.Url, createdTodo);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoGet), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Get(int id)
        {
            var todo = await _todoRepository.Get(id);

            if (todo == null)
                return NotFound();

            return Ok(todo.ToTodoGet(GetRootUrl()));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(TodoGet), 200)]
        public async Task<IActionResult> Update(int id, TodoUpdate todoUpdate)
        {
            var todo = new TodoItem()
            {
                Id = id,
                Title = todoUpdate.Title,
                Completed = todoUpdate.Completed,
                Order = todoUpdate.Order
            };
            
            await _todoRepository.Update(todo);

            return Ok(todo.ToTodoGet(GetRootUrl()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await _todoRepository.DeleteAll();
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _todoRepository.Delete(id);
            
            return Ok();
        }

        private string GetRootUrl()
        {
            var sb = new StringBuilder();
            sb.Append("https://");
            sb.Append(Request.Host);
            sb.Append("/Todo/");

            return sb.ToString();
        }
    }
}