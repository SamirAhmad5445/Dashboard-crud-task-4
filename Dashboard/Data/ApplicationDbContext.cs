using Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Shop.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    // product & company tables
    public DbSet<Company> companies { get; set; }
    public DbSet<Product> products { get; set; }

    // post and category tables
    public DbSet<Post> posts { get; set; }
    public DbSet<Category> category { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
      mb.Entity<Company>().HasData(
          new Company { Id = 1, Name = "Nike" },
          new Company { Id = 2, Name = "Adidias" }
      );

      mb.Entity<Category>().HasData(
          new Category { Id = 1, Name = "Drama" },
          new Category { Id = 2, Name = "Action" },
          new Category { Id = 3, Name = "Horror" },
          new Category { Id = 4, Name = "Comedy" },
          new Category { Id = 5, Name = "Sci-Fi" }
      );
    }
  }
}
