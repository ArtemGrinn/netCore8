using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly ILoggerService _logger;
    private readonly IToDoService _service;

    public ToDoListController(ILoggerService logger, IToDoService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IEnumerable<ToDoItem> Get()
    {
        _logger.AddMessage($"Logger Id: {_logger.GetHashCode()}, Service Id: {_service.GetHashCode()}");
        return _service.GetList("api");
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public ToDoItem? Get(int id)
    {
        return _service.FindItemById(id);
    }
    
    [HttpPost]
    public void Post(string text)
    { 
        _service.AddItem("api", text);
    }
    
    [HttpPut]
    public void Put(ToDoItem item)
    { 
        _service.EditItem(item);
    }
    
    [HttpDelete]
    public void Delete(int id)
    { 
        _service.DeleteItem(id);
    }
}