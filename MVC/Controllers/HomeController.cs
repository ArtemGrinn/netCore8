using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IToDoService _service;

    public HomeController(ILogger<HomeController> logger, IToDoService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_service.GetList());
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View(_service.FindItemById(id));
    }
    
    [HttpPost]
    public IActionResult Add(ToDoItem item)
    { 
        _service.AddItem(item.Text, item.IsCompleted);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Edit(ToDoItem item)
    { 
        _service.EditItem(item);
        return RedirectToAction("Index");
    }
}