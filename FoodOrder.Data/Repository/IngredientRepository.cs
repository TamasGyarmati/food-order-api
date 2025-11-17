using FoodOrder.Entities.Models;

namespace FoodOrder.Data.Repository;

public interface IIngredientRepository
{
    IEnumerable<Ingredient> ReadAll();
    Task<Ingredient?> ReadById(string id);
    Task<Ingredient> Create(Ingredient ingredient);
    Task Update(Ingredient ingredient);
    Task<bool> Delete(string id);
}

public class IngredientRepository : IIngredientRepository
{
    readonly FoodDbContext _context;
    
    public IngredientRepository(FoodDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Ingredient> ReadAll() => _context.Ingredients.ToList();

    public async Task<Ingredient?> ReadById(string id) => await _context.Ingredients.FindAsync(id); 
    
    public async Task<Ingredient> Create(Ingredient entity)
    {
        var ingredient = _context.Ingredients.Add(entity);
        await _context.SaveChangesAsync();
        
        return ingredient.Entity;
    }

    public async Task Update(Ingredient newIngredient)
    {
        _context.Ingredients.Update(newIngredient);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var ingredient = await ReadById(id);
        
        if (ingredient == null) return false;
        
        _context.Remove(ingredient);
        await _context.SaveChangesAsync();
        
        return true;
    }
}