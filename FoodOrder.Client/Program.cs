using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR.Client;

namespace FoodOrder.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var sw = new Stopwatch();
        bool isEnded = true;
        
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5235")
        };
        
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5235/foodHub")
            .WithAutomaticReconnect()
            .Build();
        
        // BackgroundJobMethodCalls.cs
        connection.On<string>("newOrder", (orderId) =>
        {
            Console.Clear();
            Console.WriteLine("New order created! with ID: " + orderId);
            Console.WriteLine("\nPress any key to exit...");
            sw.Stop();
            isEnded = false;
        });

        await connection.StartAsync();

        var orderCreateDto = new
        {
            foodId = new[] { "3d47c7a1-8f0f-4cb1-9b92-f5235eb3f83e" }
        };

        var json = JsonSerializer.Serialize(orderCreateDto);
        
        var sc = new StringContent(json, Encoding.UTF8, "application/json");

        Console.WriteLine("Press enter to start the order.");
        Console.ReadLine();
        
        var foodsResponse = await httpClient.PostAsync("/Order", sc);
        foodsResponse.EnsureSuccessStatusCode();
        
        sw.Start();

        Console.WriteLine("Started order creation!");
        var ts = new Task(async void () =>
        {
            while (isEnded)
            {
                Console.Clear();
                Console.WriteLine("Time passed: " + sw.Elapsed);
                await Task.Delay(50);
            }
        });

        ts.Start();
        
        Console.ReadLine();
    }
}