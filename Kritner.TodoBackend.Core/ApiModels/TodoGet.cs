namespace Kritner.TodoBackend.Core.ApiModels
{
    public record TodoGet(string Url, string Title, bool? Completed, int? Order);
}