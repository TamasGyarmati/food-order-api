using FoodOrder.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Data;

public class FoodDbContext(DbContextOptions<FoodDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Food> Foods { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Food>()
            .HasMany(f => f.Ingredients)
            .WithOne(i => i.Food)
            .HasForeignKey(i => i.FoodId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Food)
            .WithOne(f => f.Order)
            .HasForeignKey(f => f.OrderId)
            .OnDelete(DeleteBehavior.SetNull); // ne törölje ki a hozzá tartozó Food-okat
        
        var food1Id = "1e7c8b8e-4a9d-4bc4-9f18-1cc6f30edc11";
        var food2Id = "3d47c7a1-8f0f-4cb1-9b92-f5235eb3f83e";
        
        var ing1Id = "b5a1c94d-19d4-4e16-a60b-d093c8bcd451";
        var ing2Id = "8b32f9a0-59ae-417a-9c28-f6fa1f2e55ca";
        var ing3Id = "c27479a1-07ad-4a63-8c4b-0b7035e8dcc8";

        var ing4Id = "e2f1a3cd-11ea-4c92-8461-6053b9a9f9c2";
        var ing5Id = "0ffb5dc5-6a51-4ed7-94a5-41b15b5e0e8b";
        var ing6Id = "f9482b0d-3ebe-434e-8bd0-44c446a7a5b0";
        
        modelBuilder.Entity<Food>().HasData(
            new Food
            {
                Id = food1Id,
                Name = "Pizza",
                Price = 10
            },
            new Food
            {
                Id = food2Id,
                Name = "Hamburger",
                Price = 12
            }
        );
        
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient
            {
                Id = ing1Id,
                Name = "Cheese",
                CaloriePer100Gramms = 350,
                Gramms = 50,
                FoodId = food1Id
            },
            new Ingredient
            {
                Id = ing2Id,
                Name = "Tomato",
                CaloriePer100Gramms = 20,
                Gramms = 30,
                FoodId = food1Id
            },
            new Ingredient
            {
                Id = ing3Id,
                Name = "Dough",
                CaloriePer100Gramms = 250,
                Gramms = 100,
                FoodId = food1Id
            },
            
            new Ingredient
            {
                Id = ing4Id,
                Name = "Beef Patty",
                CaloriePer100Gramms = 250,
                Gramms = 120,
                FoodId = food2Id
            },
            new Ingredient
            {
                Id = ing5Id,
                Name = "Bun",
                CaloriePer100Gramms = 280,
                Gramms = 80,
                FoodId = food2Id
            },
            new Ingredient
            {
                Id = ing6Id,
                Name = "Cheese Slice",
                CaloriePer100Gramms = 330,
                Gramms = 40,
                FoodId = food2Id
            }
        );
    }
}