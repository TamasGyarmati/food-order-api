using FoodOrder.Entities;
using FoodOrder.Logic;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodController(FoodLogic logic) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllFoods()
    {
        return Ok(logic.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> CreateFood(Dtos.FoodCreateDto dto)
    {
        await logic.CreateAsync(dto);
        return Ok($"Successfully created food named: '{dto.Name}'");
    }
}