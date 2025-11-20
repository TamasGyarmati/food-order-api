using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [param: Range(1, 1000)] int Gramms);

    public record IngredientViewDto(
        string Name,
        double CaloriePer100Gramms,
        int Gramms);

    public record FoodViewDto(
        [property: JsonPropertyOrder(0)] string Id,
        [property: JsonPropertyOrder(1)] string Name,
        [property: JsonPropertyOrder(2)] string Slug,
        [property: JsonPropertyOrder(3)] double Price,
        [property: JsonPropertyOrder(4)] double Calories,
        [property: JsonPropertyOrder(6)] IEnumerable<IngredientViewDto>? Ingredients)
    {
        [property: JsonPropertyOrder(5)] public double AvgGramms { get; set; }
    };
}