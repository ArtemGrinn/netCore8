using Domain.Models;

namespace Domain.Services;

public class ToDoService : IToDoService
{
    private readonly List<ToDoItem> _items = [];

    public ToDoService()
    { 
        AddItem("BobSmith@email.com", "Создать веб-приложение");
        AddItem("BobSmith@email.com", "Создать сервис списка дел");
        AddItem("BobSmith@email.com", "Создать модель");
        AddItem("BobSmith@email.com", "Создать контроллер");
        AddItem("AliceSmith@email.com", "Добавить middleware");
        AddItem("AliceSmith@email.com", "Добавить логирование в контроллер");
    }
    
    public IEnumerable<ToDoItem> GetList(string email)
    {
        return _items.Where(x => x.Email == email);
    }

    public ToDoItem? FindItemById(int id)
    {
        return _items.FirstOrDefault(i => i.Id == id);
    }

    public void EditItem(ToDoItem item)
    {
        var index = _items.FindIndex(x => x.Id == item.Id);
        if (index < 0)
            return;
        
        _items[index] = item;
    }
    
    public void AddItem(string email, string text, bool isCompleted = false)
    { 
        var nextId = _items.Count > 0 ? _items.Max(x => x.Id) + 1 : 1;
        _items.Add(new ToDoItem
        {
            Id = nextId, 
            Text = text, 
            Email = email,
            IsCompleted = isCompleted
        });
    }
    
    public void DeleteItem(int id)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index < 0)
            return;
        
        _items.RemoveAt(index);
    }
}