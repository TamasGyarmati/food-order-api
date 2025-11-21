using FoodOrder.Data.Repository;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using FoodOrder.Logic.Helpers;

namespace FoodOrder.Logic;

public class OrderLogic(IOrderRepository repo, IFoodRepository foodRepo, DtoProvider dtoProvider)
{
    public IEnumerable<Dtos.OrderViewDto> GetAsync()
    {
        var order = repo.ReadAll();
        var orderView = order.Select(o => dtoProvider.Mapper.Map<Dtos.OrderViewDto>(o));
        return orderView;
    }
    
    public async Task<string> CreateAsync(Dtos.OrderCreateDto dto)
    {
        if (dto.foodId == null || dto.foodId.Length == 0 || dto.foodId.Any(string.IsNullOrEmpty))
            throw new Exception("You must fill at least one FoodId!");
        
        var foods = new List<Food>();
        
        foreach (var foodId in dto.foodId)
        {
            var food = await foodRepo.ReadById(foodId) ?? throw new Exception("One or more FoodId values are invalid.");
            foods.Add(food);
        }
        
        var order = new Order { Food = foods };
        
        var createdOrder = await repo.Create(order);
        
        return createdOrder.Id;
    }

    public async Task DeleteAsync(string id)
    {
        var result = await repo.Delete(id);
        if (!result) throw new NullReferenceException("Order by ID was not found.");
    }
}