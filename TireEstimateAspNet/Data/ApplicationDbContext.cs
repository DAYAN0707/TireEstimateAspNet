using Microsoft.EntityFrameworkCore;
using TireEstimateAspNet.Models;

namespace TireEstimateAspNet.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Quote> Quotes { get; set; }
}
