using AutoMapper;
using Dashboard.Models;
using Dashboard.Models.DTO;

namespace Dashboard
{
  public class AutomapperConfig : Profile
  {
    public AutomapperConfig()
    {
      CreateMap<Product, ProductDTO>().ReverseMap();
      CreateMap<Post, PostDTO>().ReverseMap();
    }
  }
}
