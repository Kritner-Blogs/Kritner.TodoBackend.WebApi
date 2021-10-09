using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kritner.TodoBackend.Core.Models;

namespace Kritner.TodoBackend.Core.Repositories
{
    public interface ITodoRepository
    {
        Task Create(TodoItem todo);
        Task Update(TodoItem todo);
        Task<TodoItem> Delete(int id);
        Task DeleteAll();
        Task<IEnumerable<TodoItem>> Get();
        Task<TodoItem> Get(int id);
    }
}