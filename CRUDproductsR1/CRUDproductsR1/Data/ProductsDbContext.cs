using System;
using CRUDproductsR1.Models;
using Microsoft.EntityFrameworkCore;
namespace CRUDproductsR1.Data;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }

    public DbSet<Product> Product { get; set; }
}
