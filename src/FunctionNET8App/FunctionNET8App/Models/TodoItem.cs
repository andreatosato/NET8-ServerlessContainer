namespace FunctionNET8App.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public DateTime DueDate { get; set; }
    public string ToDo { get; set; } = "Something";
    public string? Note { get; set; }
}
