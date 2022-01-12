using Microsoft.EntityFrameworkCore;
using SecretSanta.Models;

namespace SecretSanta.Data;

public class SantaContext : DbContext
{
    public DbSet<Receiver> Receivers { get; set; }
    public DbSet<Santa> Santas { get; set; }


    public SantaContext(DbContextOptions<SantaContext> options)
        :base(options)
    {
        Database.EnsureCreated();
    }
}