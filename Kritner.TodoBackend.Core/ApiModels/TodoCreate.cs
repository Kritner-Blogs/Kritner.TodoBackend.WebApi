using System.ComponentModel.DataAnnotations;

namespace Kritner.TodoBackend.Core.ApiModels
{
    public record TodoCreate
    {
        [Required]
        public string Title { get; set; }
        public bool Completed { get; set; }
        public int Order { get; set; }
    }
}