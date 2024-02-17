using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
  public class Post
  {
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }  
    public string Description { get; set; }
    public string Author { get; set; }
    public string Date { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category category { get; set; }
   }
}
