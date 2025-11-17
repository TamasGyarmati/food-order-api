using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodOrder.Entities.Models;

public class Ingredient
{
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 1000)]
    public double CaloriePer100Gramms { get; set; }
    
    [Required]
    [Range(1, 1000)]
    public int Gramms { get; set; }
    
    [JsonIgnore]
    public string? FoodId { get; set; }
    
    [JsonIgnore]
    public virtual Food? Food { get; set; }
}