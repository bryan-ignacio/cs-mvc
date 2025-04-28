using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDproductsR1.Models;

namespace CRUDproductsR1.Controllers;

public class SiteController : Controller
{
    public SiteController() { }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Editar()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
