using FoodOrder.Entities.Models;

namespace FoodOrder.Data.Repository;

public interface IOrderRepository
{
    IEnumerable<Order> ReadAll();
    Task<Order?> ReadById(string id);
    Task Create(Order ingredient);
    Task Update(Order ingredient);
    Task<bool> Delete(string id);
}

public class OrderRepository(FoodDbContext context) : IOrderRepository
{
    public IEnumerable<Order> ReadAll() => context.Orders.ToList();

    public async Task<Order?> ReadById(string id) => await context.Orders.FindAsync(id); 
    
    public async Task Create(Order entity)
    {
        context.Orders.Add(entity);
        await context.SaveChangesAsync();
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