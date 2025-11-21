using Microsoft.AspNetCore.SignalR;

namespace FoodOrder.Endpoint.Helpers;

public class BackgroundJobMethodCalls(IHubContext<FoodHub> foodHub)
{
    public async Task NewOrderSignal(string orderId) 
        => await foodHub.Clients.All.SendAsync("newOrder", orderId);
}