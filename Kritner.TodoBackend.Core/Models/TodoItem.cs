using System;

namespace Kritner.TodoBackend.Core.Models
{
    public record TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public int Order { get; set; }
    }
}