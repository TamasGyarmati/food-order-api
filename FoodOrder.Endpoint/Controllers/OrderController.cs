using FoodOrder.Data.Repository;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using FoodOrder.Logic.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Endpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderRepository repo, IFoodRepository foodRepo, DtoProvider dtoProvider) : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders()
    {
        var order = repo.ReadAll();
        var orderView = order.Select(o => dtoProvider.Mapper.Map<Dtos.OrderViewDto>(o));
        return Ok(orderView);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(Dtos.OrderCreateDto dto)
    {
        var foods = new List<Food>();
        
        foreach (var foodId in dto.FoodId)
        {
            var food = await foodRepo.ReadById(foodId) ?? throw new Exception("One or more FoodId values are invalid.");
            foods.Add(food);
        }
        
        var order = new Order { Food = foods };
        
        await repo.Create(order);
        return Ok($"Successfully created order: {order.Id}");
    }
}