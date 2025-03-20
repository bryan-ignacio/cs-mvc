using System;
using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Data;

public class ContactosDbContext : DbContext
{
    public ContactosDbContext(DbContextOptions<ContactosDbContext> options) : base(options) { }

    // Agregar Modelos(Cada modelo corresponda a una tabla en la base de datos)
    public DbSet<Contacto> Contactos { get; set; }

}
