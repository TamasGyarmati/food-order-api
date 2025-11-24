using FoodOrder.Entities;
using FoodOrder.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientController(IngredientLogic logic) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateIngredient(Dtos.IngredientCreateDto dto)
    {
        await logic.CreateAsync(dto);
        return Ok($"Successfully created ingredient named: '{dto.Name}'");
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task DeleteIngredient(string id) => await logic.DeleteAsync(id);
}