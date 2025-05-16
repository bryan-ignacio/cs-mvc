using System;
using CustomerCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCRUD.Data;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

    public DbSet<Customer> Customer { get; set; }
}
