using Asp.Versioning;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v2;

/// <summary>
/// API контроллер для работы с задачами версия 2
/// </summary>
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class ToDoListController : ControllerBase
{
    private readonly ILoggerService _logger;
    private readonly IToDoService _service;
 
    public ToDoListController(ILoggerService logger, IToDoService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Получить список всех задач пользователя версия 2
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "User")]
    public IEnumerable<ToDoItem> Get()
    {
        _logger.AddMessage($"Logger Id: {_logger.GetHashCode()}, Service Id: {_service.GetHashCode()}");
        return _service.GetList("api");
    }
    
    /// <summary>
    /// Получить задачу по id версия 2
    /// </summary>
    [HttpGet]
    [Route("{id:int}")]
    public ToDoItem? Get(int id)
    {
        return _service.FindItemById(id);
    }
    
    /// <summary>
    /// Добавить новую задачу версия 2
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public void Post(string text)
    { 
        _service.AddItem("api", text);
    }
    
    /// <summary>
    /// Изменить задачу версия 2
    /// </summary> 
    [HttpPut]
    public void Put(ToDoItem item)
    { 
        _service.EditItem(item);
    }
    
    /// <summary>
    /// Удалить задачу версия 2
    /// </summary>
    [HttpDelete]
    public void Delete(int id)
    { 
        _service.DeleteItem(id);
    }
}