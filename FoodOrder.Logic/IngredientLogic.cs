using FoodOrder.Data.Repository;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using FoodOrder.Logic.Helpers;

namespace FoodOrder.Logic;

public class IngredientLogic(IIngredientRepository repo, DtoProvider dtoProvider)
{
    public async Task CreateAsync(Dtos.IngredientCreateDto dto)
    {
        var ingredient = dtoProvider.Mapper.Map<Ingredient>(dto);
        await repo.Create(ingredient);
    }
}