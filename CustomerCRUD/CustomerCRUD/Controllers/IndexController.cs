using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomerCRUD.Models;
using CustomerCRUD.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerCRUD.Controllers;

public class IndexController : Controller
{
    private readonly CustomerDbContext _context;
    public IndexController(CustomerDbContext context)
    {
        this._context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await this._context.Customer.ToListAsync());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
