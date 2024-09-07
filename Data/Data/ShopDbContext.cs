using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Data.DataSeeder;

namespace Data.Data
{
    public class ShopDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ShopDbContext() { }
        public ShopDbContext(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopMvc_PV212;Integrated Security=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //DataSeederExtensions.SeedCategories(modelBuilder);
            //DataSeederExtensions.SeedProducts(modelBuilder);
            modelBuilder.SeedCategories();
            modelBuilder.SeedProducts();
        }
    }
}
