using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrder.Entities.Models;

public class Food
{
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 50)]
    public double Price { get; set; }
    
    [NotMapped]
    public double Calories
    {
        get
        {
            return Ingredients?.Sum(i => i.CaloriePer100Gramms * ((double)i.Gramms / 100)) ?? 0;
        }
    }
    
    public virtual ICollection<Ingredient>? Ingredients { get; set; } = new List<Ingredient>();
}