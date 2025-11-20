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
        var food = dtoProvider.Mapper.Map<Food>(dto);
        await repo.Create(food);
    }
}