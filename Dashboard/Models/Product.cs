namespace Dashboard.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set;}
    public string Description { get; set;}
    public int Quantity { get; set; }
    public float Price { get; set; }
    public bool EnableSize { get; set; }
    public Company company { get; set; }
  }
}
