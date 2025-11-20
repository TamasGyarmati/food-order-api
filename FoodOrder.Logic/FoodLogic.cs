using System.Text;
using FoodOrder.Data.Repository;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using FoodOrder.Logic.Helpers;

namespace FoodOrder.Logic;

public class FoodLogic(IFoodRepository repo, DtoProvider dtoProvider)
{
    public IEnumerable<Dtos.FoodViewDto> GetAll()
    {
        var foods = repo.ReadAll();

        var foodsViews = foods.Select(f => dtoProvider.Mapper.Map<Dtos.FoodViewDto>(f));

        return foodsViews;
    }
    
    public async Task CreateAsync(Dtos.FoodCreateDto dto)
    {
        char[] notAllowedChars = ['x', 'y', 'z'];
        if (dto.Name.ToLower().IndexOfAny(notAllowedChars) >= 0) throw new Exception("Food name cannot contain x, y or z");
        
        var food = dtoProvider.Mapper.Map<Food>(dto);
        await repo.Create(food);
    }

    public async Task DeleteAsync(string id)
    {
        var result = await repo.Delete(id);
        if (!result) throw new Exception("Food by ID was not found!");
    }
}