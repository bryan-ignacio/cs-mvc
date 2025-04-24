using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomerCRUD.Models;
using CustomerCRUD.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            //agregamos fecha y hora actual.
            customer.FechaCreacion = DateTime.Now;
            //agregamos el cliente que viene del form a la base de datos.
            this._context.Customer.Add(customer);
            //guardamos los datos en la base de datos.
            await this._context.SaveChangesAsync();
            //retornamos al index.
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        //buscamos el cliente por el id.
        var cliente = this._context.Customer.Find(id);
        if (cliente == null)
        {
            return NotFound();
        }
        // si se encontro se manda el cliente a la vista.
        return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Customer customer)
    {
        if (ModelState.IsValid)
        {
            // actualizamos la db con el cliente mandado desde el form.
            this._context.Update(customer);
            // guardamos los cambios en la db.
            await this._context.SaveChangesAsync();
            // luego redireccionamos a la pagina Index.
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
