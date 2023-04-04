using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafariGo.Core.Dto.Request;
using SafariGo.Core.Dto.Request.Category;
using SafariGo.Core.Models;
using SafariGo.Core.Repositories;
using SafariGo.DataAccess.Repositories;

namespace SafariGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {


        private readonly ICategoryRepositories _category;

        public CategoriesController(ICategoryRepositories category)
        {
            _category = category;
        }

        [HttpGet("allCategory")]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            var result = await _category.GetAllCategory();
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpPost("addCategory")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCategoryAsync([FromBody]CategoryRequest request)
        {
            var result = await _category.AddCategory(request);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("deleteCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategoryAsync(string id)
        {
            var result = await _category.DeleteCategory(id);

            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpPut("updateCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategoryAsync(string id, [FromBody]CategoryRequest request)
        {
            var result = await _category.UpdateCategory(id, request);

            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
