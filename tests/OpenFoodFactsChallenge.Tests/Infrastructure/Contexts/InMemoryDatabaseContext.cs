using Microsoft.EntityFrameworkCore;
using OpenFoodFactsChallenge.Infrastructure.Contexts;

namespace OpenFoodFactsChallenge.Tests.Infrastructure.Contexts;

public class InMemoryDatabaseContext : DbContext
{
    public static AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder()
            .UseInMemoryDatabase("OpenFoodFactsChallenge")
            .Options;

        var context = new AppDbContext(options);

        context.Database.EnsureCreated();

        return context;
    }
}