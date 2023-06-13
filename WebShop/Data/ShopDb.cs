using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebShop.Models.Mappings;
using WebShop.Models.ShopEntities;
using WebShop.Models.UserEntities;

namespace WebShop.Data
{
    public class ShopDb : DbContext
    {
        private readonly IConfiguration _config;


        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }


        public DbSet<ProductsOrdersMapping> ProductsOrdersMapping { get; set; }
        public DbSet<ProductsCategoriesMapping> ProductsCategoriesMapping { get; set; }




        public ShopDb(IConfiguration config, DbContextOptions options) : base(options)
        {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlite(_config.GetConnectionString("DefaultConnection"));
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProductsCategoriesMapping>().HasKey(pc => new { pc.CategoryId, pc.ProductId });

            modelBuilder.Entity<ProductsOrdersMapping>().HasKey(x => new { x.ProductId, x.OrderId });


            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categorys)
                .WithMany(c => c.Products)
                .UsingEntity<ProductsCategoriesMapping>(
                    j => j.HasOne(pc => pc.Category).WithMany(),
                    j => j.HasOne(pc => pc.Product).WithMany(),
                    j => j.HasKey(pc => new { pc.ProductId, pc.CategoryId })
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}