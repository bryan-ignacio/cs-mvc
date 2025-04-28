using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDproductsR1.Models;
using CRUDproductsR1.Data;

namespace CRUDproductsR1.Controllers;

public class SiteController : Controller
{

    private readonly ProductsDbContext _contexto;

    public SiteController(ProductsDbContext context)
    {
        this._contexto = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
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
