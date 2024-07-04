using System.ComponentModel.DataAnnotations;

namespace Papara.API.Models
{

    public class Book
    {

    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }
  }
}
