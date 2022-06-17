#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required(ErrorMessage ="Name is Required")]
    public string Name { get; set; } 
    [Required(ErrorMessage = "Chef is Required!")]
    public string Chef { get; set; }
    [Required(ErrorMessage ="Must Pick!")]
    [Range(1,5, ErrorMessage ="Must Pick!")]
    public int Tastiness { get; set; }
    [Required]
    [Range (1, Int32.MaxValue)]
    public int Calories {get;set;}
    [Required]
    public string Description {get;set;}
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}