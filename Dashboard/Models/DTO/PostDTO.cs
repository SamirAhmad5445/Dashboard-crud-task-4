using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models.DTO
{
  public class PostDTO
  {
    public int Id { get; set; }
    [Required]
    [RegularExpression(@"[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Invalid Title")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Please enter description between 5 and 500 letter")]
    [MinLength(5)]
    [MaxLength(500)]
    public string Description { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public Category? category { get; set; }
  }
}
