using System.Reflection;
using Automation.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Automation.Repository.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Money> Monies { get; set; }
    public DbSet<Tape> Tapes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }


}