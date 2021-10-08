namespace Kritner.TodoBackend.Core.ApiModels
{
    public record TodoCreateUpdate
    {
        public string Title { get; set; }
        public bool? Completed { get; set; }
        public int? Order { get; set; }
    }
}