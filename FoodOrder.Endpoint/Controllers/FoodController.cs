using FoodOrder.Data.Repository;
using FoodOrder.Endpoint.Helpers;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodController(IFoodRepository repo, DtoProvider dtoProvider) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllFoods()
    {
        var foods = repo.ReadAll();

        var foodsViews = foods.Select(f => dtoProvider.Mapper.Map<Dtos.FoodViewDto>(f));
        
        return Ok(foodsViews);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFood(Dtos.FoodCreateDto dto)
    {
        var food = dtoProvider.Mapper.Map<Food>(dto);
        
        await repo.Create(food);
        
        return Ok($"Successfully created food named: '{dto.Name}'");
    }
}