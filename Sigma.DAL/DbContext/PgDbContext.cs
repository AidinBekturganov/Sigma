namespace Sigma.DAL.DbContext;

using Sigma.Domain.Entity;
using Microsoft.EntityFrameworkCore;

public class PgDbContext : DbContext
{
    public PgDbContext(DbContextOptions options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Client> Clients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>().HasIndex(c => c.Email).IsUnique();
    }
}