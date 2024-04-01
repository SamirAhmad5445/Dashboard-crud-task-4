using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models.DTO
{
  public class ProductDTO
  {
    public int Id { get; set; }
    [Required]
    [RegularExpression(@"[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Invalid Name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter description between 5 and 500 letter")]
    [MinLength(5)]
    [MaxLength(500)]
    public string Description { get; set; }
    [Required(ErrorMessage = "Invalid Price")]
    [Range(1, 50000, ErrorMessage = "Please enter price between 1 and 50000 letter")]
    public float Price { get; set; }
    [Required]
    public Company? company { get; set; }
  }
}
