using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models {
  public class AppDbContext : DbContext {
    public DbSet<Entity> Entities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
      Database.EnsureCreated();
    }
  }
}
