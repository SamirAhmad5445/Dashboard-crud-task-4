using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using System.Diagnostics;

namespace Dashboard.Controllers
{
  public class HomeController : Controller
  {
    //private static List<Product> _products = new List<Product>();
    //private static List<Post> _posts = new List<Post>();
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
