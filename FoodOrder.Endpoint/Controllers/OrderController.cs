using System.Security.Claims;
using FoodOrder.Endpoint.Helpers;
using FoodOrder.Entities;
using FoodOrder.Logic;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(
    OrderLogic logic,
    IBackgroundJobClient backgroundJobClient,
    BackgroundJobMethodCalls jobMethods) : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders() => Ok(logic.GetAsync());
    
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrder(Dtos.OrderCreateDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var orderId = await logic.CreateAsync(dto, userId);
        
        backgroundJobClient.Schedule(() 
            => jobMethods.NewOrderSignal(orderId),
            TimeSpan.FromSeconds(dto.delay));
        
        return Ok($"Successfully created order: {orderId}");
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task DeleteOrder(string id) => await logic.DeleteAsync(id);
}