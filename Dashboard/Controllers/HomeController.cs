using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dashboard.Controllers
{
  public class HomeController : Controller
  {
    private static List<Product> _products = new List<Product>();
    private static List<Post> _posts = new List<Post>();
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
    public IActionResult AddProduct(Product newProduct)
    {
      int id;
      if(_products.Count == 0)
      {
        id = 1;
      } else
      {
        id = _products.Max(p => p.Id) + 1;
      }
      newProduct.Id = id;
      _products.Add(newProduct);
      return RedirectToAction("ViewProduct");
    }
    #endregion

    #region Read Operation
    public IActionResult ViewProduct()
    {
      return View(_products);
    }
    #endregion

    #region Update Operation
    public IActionResult EditProduct(int productId)
    {
      Product product = _products.FirstOrDefault(p => p.Id == productId);
      return View(product);
    }

    [HttpPost]
    public IActionResult EditProduct(Product product)
    {
      Product updatedProduct = _products.FirstOrDefault(p => p.Id == product.Id);
      updatedProduct.Name = product.Name;
      updatedProduct.Description = product.Description;
      updatedProduct.Quantity = product.Quantity;
      updatedProduct.Price = product.Price;
      updatedProduct.EnableSize = product.EnableSize;
      updatedProduct.company.Id = product.company.Id;

      return RedirectToAction("ViewProduct");

    }
    #endregion

    #region Delete Operation
    public IActionResult DeleteProduct(int productId)
    {
      _products.Remove(
        _products.FirstOrDefault(p => p.Id == productId));
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
    public IActionResult AddPost(Post newPost)
    {
      int id;
      if(_posts.Count == 0)
      {
        id = 1;
      } else
      {
        id = _posts.Max(p => p.Id) + 1;
      }
      newPost.Id = id;
      _posts.Add(newPost);
      return RedirectToAction("ViewBlog");
    }
    #endregion

    #region Read Operation
    public IActionResult ViewBlog()
    {
      return View(_posts);
    }
    #endregion

    #region Update Operation
    public IActionResult EditPost(int postId)
    {
      Post post = _posts.FirstOrDefault(p => p.Id == postId);
      return View(post);
    }
    [HttpPost]
    public IActionResult EditPost(Post post)
    {
      Post updatedPost = _posts.FirstOrDefault(p => p.Id == post.Id);
      updatedPost.Title = post.Title;
      updatedPost.Description = post.Description;
      updatedPost.Author = post.Author;
      updatedPost.Category = post.Category;
      updatedPost.Date = post.Date;

      return RedirectToAction("ViewBlog");
    }
    #endregion

    #region Delete Operation
    public IActionResult DeletePost(int postId)
    {
      Post postToDelete = _posts.FirstOrDefault(p => p.Id == postId);
      _posts.Remove(postToDelete);
      return RedirectToAction("ViewBlog");
    }
    #endregion


  }
}
