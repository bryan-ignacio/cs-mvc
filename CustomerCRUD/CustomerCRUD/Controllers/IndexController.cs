using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomerCRUD.Models;
using CustomerCRUD.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CustomerCRUD.Controllers;

public class IndexController : Controller
{
    private readonly CustomerDbContext _context;
    public IndexController(CustomerDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await this._context.Customer.ToListAsync());
    }

    // este metodo se encarga de mostrar la pagina donde esta el formulario.
    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Customer customer)
    {
        //validamos si todos los campos fueron llenados.
        if (ModelState.IsValid)
        {
            //agregamos el cliente que viene del form a la base de datos.
            this._context.Customer.Add(customer);
            //guardamos los datos en la base de datos.
            await this._context.SaveChangesAsync();
            //retornamos al index.
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
