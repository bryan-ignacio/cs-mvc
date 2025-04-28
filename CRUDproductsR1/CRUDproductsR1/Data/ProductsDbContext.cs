using System;
using Microsoft.EntityFrameworkCore;
namespace CRUDproductsR1.Data;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<DbContext> options) : base(options) { }
}
