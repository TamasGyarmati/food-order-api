using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Entities.Models;

public class Order
{
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public virtual ICollection<Food>? Food { get; set; }
    public string? AppUserId { get; set; }
    public virtual AppUser? AppUser { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}