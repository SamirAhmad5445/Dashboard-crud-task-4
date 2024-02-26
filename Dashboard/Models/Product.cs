using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
  public class Product
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [RegularExpression(@"[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Invalid Name")]
    public string Name { get; set;}
    [Required(ErrorMessage = "Please enter description between 5 and 500 letter")]
    [MinLength(5)]
    [MaxLength(500)]
    public string Description { get; set;}
    [Required]
    public int Quantity { get; set; }
    [Required(ErrorMessage = "Invalid Price")]
    [Range(1, 50000, ErrorMessage = "Please enter price between 1 and 50000 letter")]
    [CheckMaxCompanyPriceAttribute(30000)]
    public float Price { get; set; }
    public bool EnableSize { get; set; }
    [Required]
    public int CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    public Company? company { get; set; }
  }
}
