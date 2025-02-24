using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}