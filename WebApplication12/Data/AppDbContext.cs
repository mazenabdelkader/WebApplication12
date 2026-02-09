using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication12.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SkinImage> SkinImages { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
