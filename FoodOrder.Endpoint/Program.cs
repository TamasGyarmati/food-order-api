using FoodOrder.Data;
using FoodOrder.Data.Repository;
using FoodOrder.Endpoint.Helpers;
using FoodOrder.Logic;
using FoodOrder.Logic.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Endpoint;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddRouting();

        builder.Services.AddControllers(opt =>
        {
            opt.Filters.Add<ExceptionFilter>();
            opt.Filters.Add<ValidationFilter>();
        });
        
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        builder.Services.AddTransient<IFoodRepository, FoodRepository>();
        builder.Services.AddTransient<IIngredientRepository, IngredientRepository>();
        builder.Services.AddTransient<IOrderRepository, OrderRepository>();
        builder.Services.AddTransient<FoodLogic>();
        builder.Services.AddTransient<IngredientLogic>();
        builder.Services.AddTransient<OrderLogic>();
        builder.Services.AddTransient<DtoProvider>();
        builder.Services.AddTransient<FoodHub>();
        builder.Services.AddTransient<BackgroundJobMethodCalls>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<FoodDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration["CustomConnectionStrings:FoodDb"])
                .UseLazyLoadingProxies();
        });

        builder.Services.AddHangfire(config =>
            config.UseSqlServerStorage(builder.Configuration["CustomConnectionStrings:HangfireDb"]));
        
        builder.Services.AddHangfireServer();

        builder.Services.AddSignalR();
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.MapControllers();
        
        app.UseAuthorization();

        app.MapHub<FoodHub>("/foodHub");
        
        // Ez elavult
        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapHub<FoodHub>("/foodHub");
        // });
        
        app.UseHangfireDashboard();
        
        app.Run();
    }
}