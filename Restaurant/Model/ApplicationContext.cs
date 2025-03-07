using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<DishType>? DishTypes { get; set; }
        public DbSet<Dish>? Dishes { get; set; }
        public DbSet<Ingredient>? Ingredients { get; set; }
        public DbSet<Dish_Ingredient>? Dish_Ingredients { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Order_Dish>? Order_Dishes { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //DI
            modelBuilder.Entity<Dish_Ingredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            modelBuilder.Entity<Dish_Ingredient>()
                .HasOne(di => di.Dish)
                .WithMany()
                .HasForeignKey(di => di.DishId);

            modelBuilder.Entity<Dish_Ingredient>()
                .HasOne(di => di.Ingredient)
                .WithMany()
                .HasForeignKey(di => di.IngredientId);

            //OD
            modelBuilder.Entity<Order_Dish>()
                .HasKey(od => new { od.OrderId, od.DishId });

            modelBuilder.Entity<Order_Dish>()
                .HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Order_Dish>()
                .HasOne(od => od.Dish)
                .WithMany()
                .HasForeignKey(od => od.DishId);
        }
    }
}