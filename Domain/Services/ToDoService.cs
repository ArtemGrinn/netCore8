using Domain.Models;

namespace Domain.Services;

public class ToDoService : IToDoService
{
    private readonly List<ToDoItem> _items = [];

    public ToDoService()
    { 
        AddItem("Создать веб-приложение");
        AddItem("Создать сервис списка дел");
        AddItem("Создать модель");
        AddItem("Создать контроллер");
        AddItem("Добавить middleware");
        AddItem("Добавить логирование в контроллер");
    }
    
    public IEnumerable<ToDoItem> GetList()
    {
        return _items;
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
    
    public void AddItem(string text)
    { 
        var nextId = _items.Count > 0 ? _items.Max(x => x.Id) + 1 : 1;
        _items.Add(new ToDoItem(nextId, text));
    }
    
    public void DeleteItem(int id)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index < 0)
            return;
        
        _items.RemoveAt(index);
    }
}