using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebApplication7_petPals.Models;

namespace WebApplication7_petPals
{
    public class AppDbContext:DbContext

    {
        private readonly IConfiguration _config;
        private readonly string _ConnectionString;

        public AppDbContext(IConfiguration config)
        {
            _config = config;
            _ConnectionString = _config["ConnectionStrings:DefaultConnection"];
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Category> catogories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Wishlists> wishlists { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_ConnectionString);
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(e => e.Role)
                .HasDefaultValue("User");

            //modelBuilder.Entity<User>()
            //  .Property(e=>e.blocked)
            //  .HasDefaultValue(false);



          modelBuilder.Entity<Category>()
                .HasMany(c =>c. Products)
                .WithOne(p => p.Category)
                .HasForeignKey(c => c.CategoryId);
           


            modelBuilder.Entity<User>()
                .HasOne(c=>c.Cart)
                .WithOne(u=>u.User)
                .HasForeignKey<Cart>(u=>u.User_id);
          

            modelBuilder.Entity<Cart>()
                 .HasMany(c => c.CartItems)
                 .WithOne(c => c.Cart)
                 .HasForeignKey(c => c.Cart_id);
            modelBuilder.Entity<CartItem>()
                .HasOne(c=>c.Product)
                .WithMany(p=> p.CartItems)
                .HasForeignKey(c=>c.Product_id);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.User_id);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(o => o.Order_id);
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.Product_id);

            modelBuilder.Entity<Wishlists>()
                .HasOne(c => c.User)
                .WithMany(p => p.Wishlists)
                .HasForeignKey(c => c.User_id);
            modelBuilder.Entity<Wishlists>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.Product_id);

            base.OnModelCreating(modelBuilder);


        }

    }
}
