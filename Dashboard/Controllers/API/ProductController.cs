using AutoMapper;
using Azure.Messaging;
using Dashboard.Models;
using Dashboard.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Dashboard.Controllers.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController(ApplicationDbContext _db, IMapper _mapper) : ControllerBase
  {
    #region Creat Product - POST
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddProduct(ProductDTO? newProduct)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest("failed to insert new product");
      }

      _db.products.Add(_mapper.Map<Product>(newProduct));
      _db.SaveChanges();
      return Ok();
    }
    #endregion

    #region Read Product - GET
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
    public ActionResult<List<Product>> GetProduct()
    {
      var products = _db.products.Include(p => p.company).ToList();
      return Ok(_mapper.Map<List<ProductDTO>>(products));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    public ActionResult<Product> GetProduct(int id)
    {
      if (id <= 0) {
        return BadRequest();
      }

      var product = _db.products.Include(p => p.company).SingleOrDefault(p => p.Id == id);

      if (product == null) {
        return NotFound();
      }

      return Ok(_mapper.Map<ProductDTO>(product));
    }
    #endregion

    #region Update Product - PUT
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EditProduct(ProductDTO product)
    {
      if (product.Id <= 0 && !ModelState.IsValid)
      {
        return BadRequest("failed to update the product");
      }

      Product? updatedProduct = _db.products.FirstOrDefault(x => x.Id == product.Id);

      if (updatedProduct == null)
      {
        return NotFound();
      }
      _mapper.Map<ProductDTO, Product>(product, updatedProduct);
      _db.products.Update(updatedProduct);
      _db.SaveChanges();

      return Ok();
    }
    #endregion

    #region Delete Product - DELETE
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
    public IActionResult DeleteProduct(int id)
    {
      if(id <= 0)
      {
        return BadRequest();
      }

      Product? product = _db.products.SingleOrDefault(x => x.Id == id);

      if(product == null)
      {
        return NotFound();
      }

      _db.products.Remove(product);
      _db.SaveChanges();
      return Ok(_mapper.Map<ProductDTO>(product));
    }
    #endregion
  }
}
