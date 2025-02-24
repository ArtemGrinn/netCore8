using Domain.Models;

namespace Domain.Services;

public interface IToDoService
{
    public IEnumerable<ToDoItem> GetList(string email);
    public ToDoItem? FindItemById(int id);
    public void EditItem(ToDoItem item);
    public void AddItem(string email, string text, bool isCompleted = false);
    public void DeleteItem(int id);
}