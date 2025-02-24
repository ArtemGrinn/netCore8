using Domain.Models;
using Domain.Services;
using Duende.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

[Authorize]
public class ToDoController : Controller
{
    private readonly IToDoService _service;

    public ToDoController(IToDoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var user = User.FindFirst(JwtClaimTypes.Email)?.Value;
        return View(_service.GetList(user));
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
        var user = User.FindFirst(JwtClaimTypes.Email)?.Value;
        _service.AddItem(user, item.Text, item.IsCompleted);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Edit(ToDoItem item)
    { 
        _service.EditItem(item);
        return RedirectToAction("Index");
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}