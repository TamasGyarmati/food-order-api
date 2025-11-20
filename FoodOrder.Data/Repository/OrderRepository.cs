using FoodOrder.Entities.Models;

namespace FoodOrder.Data.Repository;

public interface IOrderRepository
{
    IEnumerable<Order> ReadAll();
    Task<Order?> ReadById(string id);
    Task<Order> Create(Order ingredient);
    Task Update(Order ingredient);
    Task<bool> Delete(string id);
}

public class OrderRepository(FoodDbContext context) : IOrderRepository
{
    public IEnumerable<Order> ReadAll() => context.Orders.ToList();

    public async Task<Order?> ReadById(string id) => await context.Orders.FindAsync(id); 
    
    public async Task<Order> Create(Order entity)
    {
        var order = context.Orders.Add(entity);
        await context.SaveChangesAsync();
        return order.Entity;
    }

    public async Task Update(Order newOrder)
    {
        context.Orders.Update(newOrder);
        await context.SaveChangesAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var order = await ReadById(id);
        
        if (order == null) return false;
        
        context.Remove(order);
        await context.SaveChangesAsync();
        
        return true;
    }
}