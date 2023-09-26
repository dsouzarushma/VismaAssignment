using FakeBloggingSystem.CustomExceptions;
using FakeBloggingSystem.DBContext;
using FakeBloggingSystem.Models.DTO;
using FakeBloggingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Transactions;

namespace FakeBloggingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) 
        { 
            _postService = postService;
        }
        /// <summary>
        /// This API endpoint returns the post by ID and it also has option to return details of the author if requested. 
        /// </summary>
        /// <param name="id">Post ID to retrieve the post</param>
        /// <param name="authorFlag">Flag to notify the end point whether author details are required or not.
        /// True: Author details are included in response 
        /// False(default):Only post details are returned</param>
        /// <returns>{"isSuccess": true,"post": null,"postAuthor": {"authorId": 1,"title": "First Post","description": "This is my first post","content": "Testing Post API","author": {"name": "Rushma", "surName": "Lopes"} }}</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPosts(Guid id, bool authorFlag)
        {
            try
            {
                var result = await _postService.GetPost(id, authorFlag);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (InvalidInputException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// This API endpoint is used to add new post.
        /// </summary>
        /// <param name="postDTO">{"authorId": 1,"title": "First Post","description": "This is my first post", "content": "Testing Post API"}</param>
        /// <returns>{"id": "a8b36f84-83fc-4fee-837f-cf0d67e91030","authorId": 1,"title": "First Post","description": "This is my first post","content": "Testing Post API"}</returns>
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost([FromBody] AddPostDTO postDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    var result = await _postService.AddPost(postDTO);
                    return Ok(result);
                }
            }
            catch (InvalidInputException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Unexpected Error occurred:"+ex.Message);
            }
        }
    }
}
