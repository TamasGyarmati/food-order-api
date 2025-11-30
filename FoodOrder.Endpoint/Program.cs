using System.Text;
using FoodOrder.Data;
using FoodOrder.Data.Repository;
using FoodOrder.Endpoint.Helpers;
using FoodOrder.Entities.Models;
using FoodOrder.Logic;
using FoodOrder.Logic.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        
        
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodOrder API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    []
                }
            });
        });
        
        builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<FoodDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddDbContext<FoodDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration["CustomConnectionStrings:FoodDb"])
                .UseLazyLoadingProxies();
        });

        builder.Services.AddHangfire(config =>
            config.UseSqlServerStorage(builder.Configuration["CustomConnectionStrings:HangfireDb"]));
        builder.Services.AddHangfireServer();
        builder.Services.AddSignalR();
        
        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = "foodorder.com",
                ValidIssuer = "foodorder.com",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"] ?? throw new Exception("jwt:key not found in appsettings.json")))
            };
        });
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.MapControllers();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHub<FoodHub>("/foodHub");
        
        app.UseHangfireDashboard();
        
        app.Run();
    }
}