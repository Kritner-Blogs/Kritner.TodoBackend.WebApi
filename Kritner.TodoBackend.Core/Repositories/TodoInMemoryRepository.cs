using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kritner.TodoBackend.Core.Models;

namespace Kritner.TodoBackend.Core.Repositories
{
    public class TodoInMemoryRepository : ITodoRepository
    {
        private readonly Dictionary<int, TodoItem> _items = new();
        
        public Task Create(TodoItem todo)
        {
            _items.Add(todo.Id, todo);
            
            return Task.CompletedTask;
        }

        public Task Update(TodoItem todo)
        {
            _items[todo.Id] = todo;
            
            return Task.CompletedTask;
        }

        public Task<TodoItem> Delete(int id)
        {
            if (_items.TryGetValue(id, out var result))
            {
                _items.Remove(id);
                return Task.FromResult(result);
            }

            return null;
        }

        public Task DeleteAll()
        {
            _items.Clear();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<TodoItem>> Get()
        {
            return Task.FromResult(_items.Select(s => s.Value));
        }

        public Task<TodoItem> Get(int id)
        {
            _items.TryGetValue(id, out var result);
            return Task.FromResult(result);
        }
    }
}