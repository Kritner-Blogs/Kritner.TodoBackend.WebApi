namespace Kritner.TodoBackend.Core.ApiModels
{
    public record TodoUpdate
    {
        public string Title { get; set; }
        public bool Completed { get; set; }
        public int Order { get; set; }
    }
}