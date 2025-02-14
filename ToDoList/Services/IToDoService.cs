using ToDoList.Models;

namespace ToDoList.Services;

public interface IToDoService
{
    public IEnumerable<ToDoItem> GetList();
    public ToDoItem? FindItemById(int id);
    public void EditItem(ToDoItem item);
    public void AddItem(string text);
    public void DeleteItem(int id);
}