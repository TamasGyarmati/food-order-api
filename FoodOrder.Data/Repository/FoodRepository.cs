using FoodOrder.Entities.Models;

namespace FoodOrder.Data.Repository;

public interface IFoodRepository
{
    IEnumerable<Food> ReadAll();
    Task<Food?> ReadById(string id);
    Task<Food> Create(Food food);
    Task Update(Food food);
    Task<bool> Delete(string id);
}

public class FoodRepository : IFoodRepository
{
    readonly FoodDbContext _context;
    
    public FoodRepository(FoodDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Food> ReadAll() => _context.Foods.ToList();

    public async Task<Food?> ReadById(string id) => await _context.Foods.FindAsync(id); 
    
    public async Task<Food> Create(Food entity)
    {
        var food = _context.Foods.Add(entity);
        await _context.SaveChangesAsync();
        
        return food.Entity;
    }

    public async Task Update(Food newFood)
    {
        _context.Foods.Update(newFood);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var food = await ReadById(id);
        
        if (food == null) return false;
        
        _context.Remove(food);
        await _context.SaveChangesAsync();
        
        return true;
    }
}