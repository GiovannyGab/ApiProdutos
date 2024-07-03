using apiFornecedor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : IdentityDbContext
{
    public ApiDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
}
