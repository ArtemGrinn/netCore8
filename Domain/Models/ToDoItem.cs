namespace Domain.Models;

public class ToDoItem(int id, string text)
{
    public int Id { get; set; } = id;

    public string? Text { get; set; } = text;

    public bool IsCompleted { get; set; } = false;
}