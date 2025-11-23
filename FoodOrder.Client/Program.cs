using System.Diagnostics;
using Microsoft.AspNetCore.SignalR.Client;

namespace FoodOrder.Client;

class Program
{
    static async Task Main()
    {
        #region OldProgram
        // var sw = new Stopwatch();
        // bool isEnded = true;
        //
        // var httpClient = new HttpClient
        // {
        //     BaseAddress = new Uri("http://localhost:5235")
        // };
        //
        // var connection = new HubConnectionBuilder()
        //     .WithUrl("http://localhost:5235/foodHub")
        //     .WithAutomaticReconnect()
        //     .Build();
        //
        // // BackgroundJobMethodCalls.cs
        // connection.On<string>("newOrder", orderId =>
        // {
        //     Console.Clear();
        //     Console.WriteLine("New order created! with ID: " + orderId);
        //     Console.WriteLine("\nPress any key to exit...");
        //     sw.Stop();
        //     isEnded = false;
        // });
        //
        // await connection.StartAsync();
        //
        // Console.WriteLine("Delay: ");
        // var delayFromConsole = int.Parse(Console.ReadLine() ?? string.Empty);
        //
        // var orderCreateDto = new
        // {
        //     foodId = new[] { "3d47c7a1-8f0f-4cb1-9b92-f5235eb3f83e" },
        //     delay = delayFromConsole
        // };
        //
        // var json = JsonSerializer.Serialize(orderCreateDto);
        //
        // var sc = new StringContent(json, Encoding.UTF8, "application/json");
        //
        // Console.WriteLine("\nPress enter to start the order.");
        // Console.ReadLine();
        //
        // var foodsResponse = await httpClient.PostAsync("/Order", sc);
        // foodsResponse.EnsureSuccessStatusCode();
        //
        // sw.Start();
        //
        // Console.WriteLine("Started order creation!");
        //
        // var stopWatchTask = new Task(async void () =>
        // {
        //     try
        //     {
        //         while (isEnded)
        //         {
        //             Console.Clear();
        //             Console.WriteLine("Time passed: " + sw.Elapsed);
        //             await Task.Delay(50);
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e.Message);
        //     }
        // });
        //
        // stopWatchTask.Start();
        //
        // Console.ReadLine();
        #endregion

        #region NewProgram

        var httpClient = new HttpClient();
        var client = new Client("http://localhost:5235", httpClient);
            
        var sw = new Stopwatch();
        bool isEnded = true;
        
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5235/foodHub")
            .WithAutomaticReconnect()
            .Build();
        
        // BackgroundJobMethodCalls.cs
        connection.On<string>("newOrder", orderId =>
        {
            Console.Clear();
            Console.WriteLine("New order created! with ID: " + orderId);
            Console.WriteLine("\nPress any key to exit...");
            sw.Stop();
            isEnded = false;
        });

        await connection.StartAsync();

        Console.WriteLine("Delay: ");
        var delayFromConsole = int.Parse(Console.ReadLine() ?? string.Empty);
        
        Console.WriteLine("\nPress enter to start the order.");
        Console.ReadLine();

        // Ez eredetileg record, viszont a Client.cs-beli generált DTO-t használjuk ami sima class 
        var dto = new OrderCreateDto
        {
            FoodId = ["3d47c7a1-8f0f-4cb1-9b92-f5235eb3f83e"],
            Delay = delayFromConsole
        };
        
        await client.OrderPOSTAsync(dto);
        
        sw.Start();

        Console.WriteLine("Started order creation!");
        
        var stopWatchTask = new Task(async void () =>
        {
            try
            {
                while (isEnded)
                {
                    Console.Clear();
                    Console.WriteLine("Time passed: " + sw.Elapsed);
                    await Task.Delay(50);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        });

        stopWatchTask.Start();
        
        Console.ReadLine();
        #endregion
    }
}