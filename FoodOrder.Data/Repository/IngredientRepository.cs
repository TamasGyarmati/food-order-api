using FoodOrder.Entities.Models;

namespace FoodOrder.Data.Repository;

public interface IIngredientRepository
{
    IEnumerable<Ingredient> ReadAll();
    Task<Ingredient?> ReadById(string id);
    Task Create(Ingredient ingredient);
    Task Update(Ingredient ingredient);
    Task<bool> Delete(string id);
}

public class IngredientRepository(FoodDbContext context) : IIngredientRepository
{
    public IEnumerable<Ingredient> ReadAll() => context.Ingredients.ToList();

    public async Task<Ingredient?> ReadById(string id) => await context.Ingredients.FindAsync(id); 
    
    public async Task Create(Ingredient entity)
    {
        context.Ingredients.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Ingredient newIngredient)
    {
        context.Ingredients.Update(newIngredient);
        await context.SaveChangesAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var ingredient = await ReadById(id);
        
        if (ingredient == null) return false;
        
        context.Remove(ingredient);
        await context.SaveChangesAsync();
        
        return true;
    }
}