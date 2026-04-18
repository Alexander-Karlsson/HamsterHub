using HamsterHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HamsterHub.Infrastructure;

public class HamsterDbContext(DbContextOptions<HamsterDbContext> options) : DbContext(options)
{
    public DbSet<Hamster> Hamsters { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ORSAK: Renare DbContext när jag lägger Entity Configs i separata filer
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HamsterDbContext).Assembly);
    }
}

