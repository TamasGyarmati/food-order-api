using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodOrder.Entities;

// param / property (recordnál param kell hiszen nem propertyk hanem primary constructor paraméterek)

public static class Dtos
{
    public record IngredientCreateDto(
        string FoodId,
        [param: Required] string Name,
        [param: Range(1, 1000)] double CaloriePer100Gramms,
        [param: Range(1, 1000)] int Gramms);

    public record IngredientViewDto(
        string Name,
        double CaloriePer100Gramms,
        int Gramms);
    
    public record FoodCreateDto(
        string Name,
        [param: Range(1, 50)] double Price);

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

    public record OrderCreateDto(string[] foodId, int delay);

    public record OrderViewDto(
        string Id,
        [property: JsonPropertyOrder(2)] DateTime OrderDate)
    {
        //[property: JsonPropertyOrder(0)] public string[] FoodId { get; set; }
        [property: JsonPropertyOrder(1)] public string[] Name { get; set; }
    }

    public record LoginResultDto(
        string AccessToken,
        DateTime AccessTokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration);
    public record TokenApiDto(
        string AccessToken,
        string RefreshToken);
    public record UserCreateDto(
        string Email,
        string Password,
        string FamilyName,
        string GivenName);
    public record UserLoginDto(
        string Email,
        string Password);
}