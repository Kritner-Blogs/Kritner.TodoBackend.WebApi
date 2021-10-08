using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kritner.TodoBackend.Core.Models;

namespace Kritner.TodoBackend.Core.Repositories
{
    public class TodoInMemoryRepository : ITodoRepository
    {
        private readonly ConcurrentDictionary<Guid, TodoItem> _items = new();
        
        public Task Create(TodoItem todo)
        {
            _items.TryAdd(todo.Id, todo);
            
            return Task.CompletedTask;
        }

        public Task Update(TodoItem todo)
        {
            _items.TryUpdate(todo.Id, todo, _items[todo.Id]);
            
            return Task.CompletedTask;
        }

        public Task<TodoItem> Delete(Guid id)
        {
            _items.TryRemove(id, out var result);
            return Task.FromResult(result);
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

        public Task<TodoItem> Get(Guid id)
        {
            _items.TryGetValue(id, out var result);
            return Task.FromResult(result);
        }
    }
}