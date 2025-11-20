using FoodOrder.Entities;
using FoodOrder.Logic;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(OrderLogic logic) : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders() => Ok(logic.GetAsync());
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(Dtos.OrderCreateDto dto)
    {
        var orderId = await logic.CreateAsync(dto);
        return Ok($"Successfully created order: {orderId}");
    }

    [HttpDelete]
    public async Task DeleteOrder(string id) => await logic.DeleteAsync(id);
}