using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhatsAppCloneMVC.Data;
using WhatsAppCloneMVC.Models;

namespace WhatsAppCloneMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ChatContext _chatContext;
    public HomeController(ILogger<HomeController> logger, ChatContext chatContext)
    {
        _logger = logger;
        _chatContext = chatContext;
    }

    public IActionResult Index()
    {
        ViewBag.UserList = JsonConvert.SerializeObject(_chatContext.Users.ToList()) ;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
