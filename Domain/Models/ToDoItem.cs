namespace Domain.Models;

public class ToDoItem
{
    public int Id { get; init; }
    public string Text { get; init; } = string.Empty;
    
    public string Email { get; init; } = string.Empty;
    public bool IsCompleted { get; init; }
}