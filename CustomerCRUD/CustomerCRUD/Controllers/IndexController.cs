using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomerCRUD.Models;

namespace CustomerCRUD.Controllers;

public class IndexController : Controller
{
    public IndexController() { }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
