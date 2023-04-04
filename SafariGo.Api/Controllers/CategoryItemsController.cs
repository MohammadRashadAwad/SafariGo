using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafariGo.Core.Dto.Request.CategoryItem;
using SafariGo.Core.Models;
using SafariGo.Core.Repositories;

namespace SafariGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryItemsController : ControllerBase
    {
        private readonly ICategoryItemRepositories _categoryItem;

        public CategoryItemsController(ICategoryItemRepositories categoryItem)
        {
            _categoryItem = categoryItem;
        }

        [HttpGet("allCategoryItem")]
        public async Task<IActionResult> GetAllCategoryItemAsync()
        {
            var result = await _categoryItem.GetAllCategoryItem();
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpPost("addCategoryItem")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCategoryItemAsync([FromForm]CategoryItemRequest request)
        {
            var result = await _categoryItem.AddCategoryItem(request);

            return result.Status ? Ok(result) : BadRequest(result) ;
        }
        [HttpDelete("deleteCategoryItem/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategoryItemAsync(string id)
        {
            var result = await _categoryItem.DeleteCategoryItem(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpPut("updateCategoryItem/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategoryItemAsync(string id, [FromForm] CategoryItemRequest request)
        {
            var result = await _categoryItem.UpdateCategoryItem(id,request);
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
