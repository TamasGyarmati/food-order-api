using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrder.Entities.Models;

public class Order
{
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string FoodId { get; set; }
    
    public virtual Food? Food { get; set; }
    
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}