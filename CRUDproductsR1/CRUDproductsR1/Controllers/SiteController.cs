using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDproductsR1.Models;
using CRUDproductsR1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        List<Product> products = await this._contexto.Product.ToListAsync();
        return View(products);
    }

    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var product = this._contexto.Product.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Product product)
    {
        if (ModelState.IsValid)
        {
            this._contexto.Update(product);
            await this._contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpGet]
    public IActionResult Info(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = this._contexto.Product.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpGet]
    public IActionResult Borrar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var product = this._contexto.Product.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Borrar(Product product)
    {
        this._contexto.Remove(product);
        await this._contexto.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Agregar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Agregar(Product product)
    {
        if (ModelState.IsValid)
        {
            this._contexto.Product.Add(product);
            await this._contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
