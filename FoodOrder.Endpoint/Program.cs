using FoodOrder.Data;
using FoodOrder.Data.Repository;
using FoodOrder.Endpoint.Helpers;
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

        builder.Services.AddTransient<IFoodRepository, FoodRepository>();
        builder.Services.AddTransient<IIngredientRepository, IngredientRepository>();
        builder.Services.AddTransient<DtoProvider>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<FoodDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration["CustomConnectionStrings:FoodDb"])
                .UseLazyLoadingProxies();
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.UseAuthorization();

        app.Run();
    }
}