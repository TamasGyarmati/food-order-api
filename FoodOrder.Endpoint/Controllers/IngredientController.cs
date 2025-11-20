using FoodOrder.Data.Repository;
using FoodOrder.Endpoint.Helpers;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientController(IIngredientRepository repo, DtoProvider dtoProvider) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateIngredient(Dtos.IngredientCreateDto dto)
    {
        // var ingredient = new Ingredient
        // {
        //     Name = dto.Name,
        //     CaloriePer100Gramms = dto.CaloriePer100Gramms,
        //     Gramms = dto.Gramms,
        //     FoodId = dto.FoodId
        // };

        var ingredient = dtoProvider.Mapper.Map<Ingredient>(dto);
        
        await repo.Create(ingredient);
        
        return Ok($"Successfully created ingredient named: '{dto.Name}'");
    }
}