using FoodOrder.Endpoint.Helpers;
using FoodOrder.Entities;
using FoodOrder.Logic;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

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
    public async Task<IActionResult> CreateOrder(Dtos.OrderCreateDto dto)
    {
        var orderId = await logic.CreateAsync(dto);
        
        backgroundJobClient.Schedule(() => jobMethods.NewOrderSignal(orderId), TimeSpan.FromSeconds(5));
        
        return Ok($"Successfully created order: {orderId}");
    }

    [HttpDelete]
    public async Task DeleteOrder(string id) => await logic.DeleteAsync(id);
}