using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafariGo.Core.Dto.Request.Posts;
using SafariGo.Core.Repositories;

namespace SafariGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepositories _posts;

        public PostsController(IPostsRepositories posts)
        {
           _posts = posts;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var result = await _posts.GetAllPosts();
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromForm]CreatePostRequest request)
        {
            var result = await _posts.CreatePostAsync(request);
            if (!result.Status)
                return BadRequest(result.Message);
            return Ok(result.Message);
        }
        [HttpPost("comment")]
        public async Task<IActionResult> CreateCommentAsync([FromForm] CommentRequest request)
        {
            var result = await _posts.CreateCommentAsync(request);
            if (!result.Status)
                return BadRequest(result.Message);
            return Ok(result.Message);
        }
        [HttpPost("like")]
        public async Task<IActionResult> AddLikeAsync([FromBody] LikeRequest request)
        {
            var result = await _posts.AddLikeAsync(request);
            if(!result.Status)
             return BadRequest(result.Message);
            return Ok(result.Message);
        }
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePostAsync(string postId)
        {
            var result = await _posts.DeletePostAsync(postId);
            if (!result.Status)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpDelete("comment/{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(string commentId)
        {
            var result = await _posts.DeleteCommentAsync(commentId);
            if (!result.Status)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpDelete("like/{likeId}")]
        public async Task<IActionResult> RemoveLikeAsync(string likeId)
        {
            var result = await _posts.RemoveLike(likeId);
            if (!result.Status)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
