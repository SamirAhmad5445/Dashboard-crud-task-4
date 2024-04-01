using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Shop.Data;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO.Pipelines;

namespace Dashboard.Controllers
{
  public class HomeController : Controller
  {
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      return View();
    }

    // CRUD Operations for the Products
    #region Create Operation
    public IActionResult AddProduct()
    {
      return View();
    }
    [HttpPost]
    public IActionResult AddProduct(Product? newProduct)
    {
      if (!ModelState.IsValid)
      {
        return View(newProduct);
      }

      _db.products.Add(newProduct);
      _db.SaveChanges();
      return RedirectToAction("ViewProduct");
    }
    #endregion

    #region Read Operation
    public IActionResult ViewProduct()
    {
      var products = _db.products.Include(p => p.company).ToList();
      return View(products);
    }
    #endregion

    #region Update Operation
    public IActionResult EditProduct(int productId)
    {
      Product? product = _db.products.SingleOrDefault(x => x.Id == productId); 
      return View(product);
    }

    [HttpPost]
    public IActionResult EditProduct(Product product)
    {
      if (!ModelState.IsValid)
      {
        return View(product);
      }

      Product? updatedProduct = _db.products.FirstOrDefault(x => x.Id == product.Id);
      updatedProduct.Name = product.Name;
      updatedProduct.Id = product.Id;
      updatedProduct.Description = product.Description;
      updatedProduct.Price = product.Price;
      updatedProduct.EnableSize = product.EnableSize;
      updatedProduct.CompanyId = product.CompanyId;
      updatedProduct.Quantity = product.Quantity;
      _db.products.Update(updatedProduct);
      _db.SaveChanges();

      return RedirectToAction("ViewProduct");
    }
    #endregion

    #region Delete Operation
    public IActionResult DeleteProduct(int productId)
    {
      Product? toDelete = _db.products.SingleOrDefault(x => x.Id == productId);
      _db.products.Remove(toDelete);
      _db.SaveChanges();
      return RedirectToAction("ViewProduct");
    }
    #endregion

    // CRUD Operation for the Blog
    #region Create Operation
    public IActionResult AddPost()
    {
      return View();
    }
    [HttpPost]
    public IActionResult AddPost(Post? newPost)
    {
      if (!ModelState.IsValid)
      {
        return View(newPost);
      }

      _db.posts.Add(newPost);
      _db.SaveChanges();
      return RedirectToAction("ViewBlog");
    }
    #endregion

    #region Read Operation
    public IActionResult ViewBlog()
    {
      var posts = _db.posts.Include(p => p.category).ToList();
      return View(posts);
    }
    #endregion

    #region Update Operation
    public IActionResult EditPost(int postId)
    {
      Post post = _db.posts.FirstOrDefault(p => p.Id == postId);
      return View(post);
    }
    [HttpPost]
    public IActionResult EditPost(Post post)
    {
      if (!ModelState.IsValid)
      {
        return View(post);
      }

      Post? updatedPost = _db.posts.FirstOrDefault(p => p.Id == post.Id);
      updatedPost.Title = post.Title;
      updatedPost.Description = post.Description;
      updatedPost.Author = post.Author;
      updatedPost.category = post.category;
      updatedPost.Date = post.Date;
			_db.posts.Update(updatedPost);
			_db.SaveChanges();

			return RedirectToAction("ViewBlog");
    }
    #endregion

    #region Delete Operation
    public IActionResult DeletePost(int postId)
    {
      Post? postToDelete = _db.posts.FirstOrDefault(p => p.Id == postId);
      _db.posts.Remove(postToDelete);
			_db.SaveChanges();
			return RedirectToAction("ViewBlog");
    }
    #endregion
  }
}

#region Custom validation for the product price
public class CheckMaxCompanyPriceAttribute : ValidationAttribute
{
  private readonly int _maxPrice;
  public CheckMaxCompanyPriceAttribute(int price)
  {
    _maxPrice = price;
  }

  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    Product product = (Product)validationContext.ObjectInstance;
    int price;
    if (!int.TryParse(value.ToString(), out price))
    {
      return new ValidationResult("Invalid Price");
    }

    if (product.CompanyId == 2 && price >= _maxPrice)
    {
      return new ValidationResult("Max Price is " + price + " for Adidas.");
    }
    return ValidationResult.Success;
  }
}
#endregion

#region Custom validation for the post date
public class CheckPublishDateAttribute : ValidationAttribute
{
  private readonly DateTime _maxDate;
  public CheckPublishDateAttribute()
  {
    _maxDate = DateTime.Today;
  }

  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    DateTime ParsedDate;
    if (!DateTime.TryParseExact((string)value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out ParsedDate))
    {
      return new ValidationResult("Invalid Date1");
    }

    if (ParsedDate > _maxDate)
    {
      return new ValidationResult("Invalid Date2");
    }

    return ValidationResult.Success;
  }
}
#endregion