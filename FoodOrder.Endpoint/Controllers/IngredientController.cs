using FoodOrder.Entities;
using FoodOrder.Logic;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientController(IngredientLogic logic) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateIngredient(Dtos.IngredientCreateDto dto)
    {
        await logic.CreateAsync(dto);
        return Ok($"Successfully created ingredient named: '{dto.Name}'");
    }
}