namespace Domain.Models;

public class ToDoItem
{
    /// <summary>
    ///  Id задачи
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Text { get; init; } = string.Empty;
    /// <summary>
    /// Email пользователя (владелец задачи)
    /// </summary>
    public string Email { get; init; } = string.Empty;
    /// <summary>
    /// Флаг завершения задачи
    /// </summary>
    public bool IsCompleted { get; init; }
}