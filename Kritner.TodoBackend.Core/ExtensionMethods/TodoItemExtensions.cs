using System.Text;
using Kritner.TodoBackend.Core.ApiModels;
using Kritner.TodoBackend.Core.Models;

namespace Kritner.TodoBackend.Core.ExtensionMethods
{
    public static class TodoItemExtensions
    {
        public static TodoGet ToTodoGet(this TodoItem todo, string rootUrl)
        {
            return new TodoGet($"{rootUrl}{todo.Id.ToString()}", todo.Title, todo.Completed, todo.Order);
        }
    }
}