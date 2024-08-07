using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
   DbSet<Game> Games { get; }
   DbSet<Order> Orders { get; }
   DbSet<OrderItem> OrderItems { get; }
   DbSet<ShippingAddress> ShippingAddresses { get; }
   DbSet<Payment> Payments { get; }
   DbSet<ApplicationUser> Users { get; }

   Task<int> SaveChangesAsync(CancellationToken ct = default);
   Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);
}