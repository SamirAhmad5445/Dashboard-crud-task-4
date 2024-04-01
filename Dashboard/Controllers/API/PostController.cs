using AutoMapper;
using Dashboard.Models;
using Dashboard.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Dashboard.Controllers.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class PostController(ApplicationDbContext _db, IMapper _mapper) : ControllerBase
  {
    #region Create Post - POST
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddPost(PostDTO? newPost)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest("failed to insert new product");
      }

      _db.posts.Add(_mapper.Map<Post>(newPost));
      _db.SaveChanges();
      return Ok();
    }
    #endregion

    #region Read Posts - GET
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PostDTO>))]
    public ActionResult<List<PostDTO>> GetPost()
    {
      var posts = _db.posts.Include(p => p.category).ToList();
      return Ok(_mapper.Map<List<PostDTO>>(posts));
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDTO))]
    public ActionResult<PostDTO> GetPost(int id)
    {
      if (id <= 0)
      {
        return BadRequest();
      }

      var post = _db.posts.SingleOrDefault(p => p.Id == id);

      if(post == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<PostDTO>(post));
    }
    #endregion

    #region Update Post - PUT
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EditPost(PostDTO post)
    {
      if (post.Id <= 0 && !ModelState.IsValid)
      {
        return BadRequest("failed to update the product");
      }

      Post? updatedPost = _db.posts.FirstOrDefault(x => x.Id == post.Id);

      if (updatedPost == null)
      {
        return NotFound();
      }
      _mapper.Map<PostDTO, Post>(post, updatedPost);
      _db.posts.Update(updatedPost);
      _db.SaveChanges();

      return Ok();
    }
    #endregion

    #region Delete post - DELETE
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDTO))]
    public IActionResult DeletePost(int id)
    {
      if (id <= 0)
      {
        return BadRequest();
      }

      Post? post = _db.posts.SingleOrDefault(x => x.Id == id);

      if (post == null)
      {
        return NotFound();
      }

      _db.posts.Remove(post);
      _db.SaveChanges();
      return Ok(_mapper.Map<PostDTO>(post));
    }
    #endregion

  }
}
