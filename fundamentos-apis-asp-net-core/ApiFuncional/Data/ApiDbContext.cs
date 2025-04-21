using ApiFuncional.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiFuncional.Data;

// public class ApiDbContext : DbContext -> antes
public class ApiDbContext : IdentityDbContext // -> antes
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {}

    public DbSet<Produto> Produtos { get; set; }
}
