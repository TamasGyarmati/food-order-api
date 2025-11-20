using FoodOrder.Data.Repository;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using FoodOrder.Logic.Helpers;

namespace FoodOrder.Logic;

public class IngredientLogic(
    IIngredientRepository repo, 
    IFoodRepository foodRepo,
    DtoProvider dtoProvider)
{
    public async Task CreateAsync(Dtos.IngredientCreateDto dto)
    {
        _ = await foodRepo.ReadById(dto.FoodId) ?? throw new NullReferenceException("Food by ID was not found!");
        var ingredient = dtoProvider.Mapper.Map<Ingredient>(dto);
        await repo.Create(ingredient);
    }

    public async Task DeleteAsync(string id)
    {
        var result = await repo.Delete(id);
        if (!result) throw new NullReferenceException("Ingredient by ID was not found!");
    }
}