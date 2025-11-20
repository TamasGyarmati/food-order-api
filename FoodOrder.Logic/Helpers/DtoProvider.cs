using AutoMapper;
using FoodOrder.Entities;
using FoodOrder.Entities.Models;
using Microsoft.Extensions.Logging;

namespace FoodOrder.Logic.Helpers;

public class DtoProvider
{
    public Mapper Mapper { get; }

    public DtoProvider()
    {
        Mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Dtos.FoodCreateDto, Food>();
            cfg.CreateMap<Food, Dtos.FoodViewDto>();
            cfg.CreateMap<Ingredient, Dtos.IngredientViewDto>();
            cfg.CreateMap<Dtos.IngredientCreateDto, Ingredient>();

        }, new LoggerFactory()));
    }
}