using FoodOrder.Entities.Models;

namespace FoodOrder.Data.Repository;

public interface IFoodRepository
{
    IEnumerable<Food> ReadAll();
    Task<Food?> ReadById(string id);
    Task Create(Food food);
    Task Update(Food food);
    Task<bool> Delete(string id);
}

public class FoodRepository(FoodDbContext context) : IFoodRepository
{
    public IEnumerable<Food> ReadAll() => context.Foods.ToList();

    public async Task<Food?> ReadById(string id) => await context.Foods.FindAsync(id); 
    
    public async Task Create(Food entity)
    {
        context.Foods.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Food newFood)
    {
        context.Foods.Update(newFood);
        await context.SaveChangesAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var food = await ReadById(id);
        
        if (food == null) return false;
        
        context.Remove(food);
        await context.SaveChangesAsync();
        
        return true;
    }
}