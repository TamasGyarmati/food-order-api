using FoodOrder.Entities.Models;

namespace FoodOrder.Entities;

public static class Dtos
{
    public record FoodCreateDto(
        string Name,
        double Price);

    public record IngredientCreateDto(
        string FoodId,
        string Name,
        double CaloriePer100Gramms,
        int Gramms);

    public record IngredientViewDto(
        string Name,
        double CaloriePer100Gramms,
        int Gramms);

    public record FoodViewDto(
        string FoodId,
        string FoodName,
        double FoodPrice,
        double Calories,
        IEnumerable<IngredientViewDto>? Ingredients);
}