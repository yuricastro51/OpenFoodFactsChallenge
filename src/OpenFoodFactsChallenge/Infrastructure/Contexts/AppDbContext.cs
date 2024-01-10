using Microsoft.EntityFrameworkCore;
using OpenFoodFactsChallenge.Infrastructure.Models;

namespace OpenFoodFactsChallenge.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<MongoProduct> Products { get; set; }
}