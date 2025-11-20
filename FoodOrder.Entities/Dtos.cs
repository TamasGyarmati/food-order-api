using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Entities;

// param / property (recordnál param kell hiszen nem propertyk hanem primary constructor paraméterek)

public static class Dtos
{
    public record FoodCreateDto(
        string Name,
        [param: Range(1, 50)] double Price);

    public record IngredientCreateDto(
        string FoodId,
        [param: Required] string Name,
        [param: Range(1, 1000)] double CaloriePer100Gramms,
        [param: Range(1, int.MaxValue)] int Gramms);

    public record IngredientViewDto(
        string Name,
        double CaloriePer100Gramms,
        int Gramms);

    public record FoodViewDto(
        string Id,
        string Name,
        double Price,
        double Calories,
        IEnumerable<IngredientViewDto>? Ingredients);
}