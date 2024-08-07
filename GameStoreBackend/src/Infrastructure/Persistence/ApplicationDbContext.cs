using System.Data.Common;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<ShippingAddress> ShippingAddresses => Set<ShippingAddress>();

    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
    {
        return await Database.BeginTransactionAsync(ct);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await base.SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.ShippingAddress)
            .WithOne()
            .HasForeignKey<ShippingAddress>(sa => sa.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Payment)
            .WithOne()
            .HasForeignKey<Payment>(p => p.OrderId);

        modelBuilder.Entity<Game>()
            .HasMany(g => g.OrderItems)
            .WithOne(oi => oi.Game)
            .HasForeignKey(oi => oi.GameId);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Orders)
            .WithOne()
            .HasForeignKey(o => o.UserId);

        base.OnModelCreating(modelBuilder);
    }
}