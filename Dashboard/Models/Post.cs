using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
  public class Post
  {
    [Key]
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
    [CheckPublishDate]
    public string Date { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category category { get; set; }
   }
}
