using Domain.Models;

namespace Domain.Services;

public interface IToDoService
{
    public IEnumerable<ToDoItem> GetList();
    public ToDoItem? FindItemById(int id);
    public void EditItem(ToDoItem item);
    public void AddItem(string text, bool isCompleted = false);
    public void DeleteItem(int id);
}