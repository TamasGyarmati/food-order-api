using FoodOrder.Data.Repository;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodController(IFoodRepository repo) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllFoods()
    {
        var foods = repo.ReadAll();
        
        var foodsViews = foods.Select(f => new Dtos.FoodViewDto(
            f.Id, 
            f.Name, 
            f.Price, 
            f.Calories, 
            f.Ingredients?.Select(i => new Dtos.IngredientViewDto(i.Name, i.CaloriePer100Gramms, i.Gramms))));
        
        return Ok(foodsViews);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFood(Dtos.FoodCreateDto dto)
    {
        var food = new Food
        {
            Name = dto.Name,
            Price = dto.Price
        };
        
        await repo.Create(food);
        
        return Ok($"Successfully created food named: '{dto.Name}'");
    }
}