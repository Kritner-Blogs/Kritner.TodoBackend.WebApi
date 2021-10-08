using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kritner.TodoBackend.Core.ApiModels;
using Kritner.TodoBackend.Core.ExtensionMethods;
using Kritner.TodoBackend.Core.Models;
using Kritner.TodoBackend.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kritner.TodoBackend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet("/")]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoRepository.Get();
            var rootUrl = GetRootUrl();
            
            return Ok(todos.ToList().Select(s => s.ToTodoGet(rootUrl)));
        }
        
        [HttpPost("/")]
        public async Task<IActionResult> Create(TodoCreateUpdate todoCreateUpdate)
        {
            var todo = new TodoItem()
            {
                Id = Guid.NewGuid(),
                Title = todoCreateUpdate.Title
            };
            
            await _todoRepository.Create(todo);
            
            return Ok(todo.ToTodoGet(GetRootUrl()));
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var todo = await _todoRepository.Get(id);

            if (todo == null)
                return NotFound();

            return Ok(todo.ToTodoGet(GetRootUrl()));
        }

        [HttpPatch("/{id}")]
        public async Task<IActionResult> Update(Guid id, TodoCreateUpdate todoCreateUpdate)
        {
            var todo = new TodoItem()
            {
                Id = id,
                Title = todoCreateUpdate.Title,
                Completed = todoCreateUpdate.Completed,
                Order = todoCreateUpdate.Order
            };
            
            await _todoRepository.Update(todo);

            return Ok(todo.ToTodoGet(GetRootUrl()));
        }

        [HttpDelete("/")]
        public async Task<IActionResult> Delete()
        {
            await _todoRepository.DeleteAll();
            
            return Ok();
        }
        
        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _todoRepository.Delete(id);
            
            return Ok();
        }

        private string GetRootUrl()
        {
            var sb = new StringBuilder();
            sb.Append(Request.Scheme);
            sb.Append("://");
            sb.Append(Request.Host);
            sb.Append("/");

            return sb.ToString();
        }
    }
}