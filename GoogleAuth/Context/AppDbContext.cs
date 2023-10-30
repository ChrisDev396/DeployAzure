using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{}

	public DbSet<Usuario>? Usuarios { get; set; }
	public DbSet<Item>? Itens { get; set; }
}

