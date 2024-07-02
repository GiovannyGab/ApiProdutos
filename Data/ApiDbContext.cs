using apiFornecedor.Models;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
}
