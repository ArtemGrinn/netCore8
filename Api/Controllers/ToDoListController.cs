using System.Security.Claims;
using Asp.Versioning;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
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
    /// Получить список всех задач пользователя
    /// </summary>
    [HttpGet]
    public IEnumerable<ToDoItem> Get()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        _logger.AddMessage($"User name: {(User.Identity.IsAuthenticated ? User.Identity.Name : identity.IsAuthenticated)}, Logger Id: {_logger.GetHashCode()}, Service Id: {_service.GetHashCode()}");
        return _service.GetList("api");
    }
    
    /// <summary>
    /// Получить задачу по id
    /// </summary>
    [HttpGet]
    [Route("{id:int}")]
    public ToDoItem? Get(int id)
    {
        return _service.FindItemById(id);
    }
    
    /// <summary>
    /// Добавить новую задачу
    /// </summary>
    [HttpPost]
    public void Post(string text)
    { 
        _service.AddItem("api", text);
    }
    
    /// <summary>
    /// Изменить задачу
    /// </summary> 
    [HttpPut]
    public void Put(ToDoItem item)
    { 
        _service.EditItem(item);
    }
    
    /// <summary>
    /// Удалить задачу
    /// </summary>
    [HttpDelete]
    public void Delete(int id)
    { 
        _service.DeleteItem(id);
    }
}