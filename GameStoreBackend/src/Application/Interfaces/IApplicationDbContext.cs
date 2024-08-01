using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
   DbSet<Game> Games { get; } 
   
   Task<int> SaveChangesAsync(CancellationToken ct = default);
}