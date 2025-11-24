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
            cfg.CreateMap<Dtos.FoodCreateDto, Food>()
                .AfterMap((src, dest) => dest.Slug = SlugGenerator.SlugGenerator.GenerateSlug(src.Name));
            cfg.CreateMap<Food, Dtos.FoodViewDto>()
                .AfterMap((src, dest) => dest.AvgGramms = src.Ingredients?.Count > 0 
                        ? src.Ingredients.Average(x => x.Gramms)
                        : 0);
            cfg.CreateMap<Ingredient, Dtos.IngredientViewDto>();
            cfg.CreateMap<Dtos.IngredientCreateDto, Ingredient>();
            cfg.CreateMap<Order, Dtos.OrderViewDto>()
                .AfterMap((src, dest) =>
                {
                    //dest.FoodId = src.Food?.Select(f => f.Id).ToArray() ?? [];
                    dest.FoodName = src.Food?.Select(f => f.Name).ToArray() ?? [];
                    dest.CreatorUserName = src.AppUser?.UserName ?? string.Empty;
                });

        }, new LoggerFactory()));
    }
}