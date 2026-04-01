using DemoWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebapp.Data
{
    public class TroDbContext : DbContext
    {
       
            public TroDbContext(DbContextOptions<TroDbContext> options)
                : base(options)
            {
            }
            public DbSet<User> Users { get; set; }
            public DbSet<Room> Rooms { get; set; }
            public DbSet<Tenant> Tenants { get; set; }
            public DbSet<Bill> Bills { get; set; }
            public DbSet<Service> Services { get; set; }
        }
    }

