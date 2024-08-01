using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options): base(options)
    {}
    public DbSet<Game> Games => Set<Game>();

    public async override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
       return await base.SaveChangesAsync(ct);
    }
}