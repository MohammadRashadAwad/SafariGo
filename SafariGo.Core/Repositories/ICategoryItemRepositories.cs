using SafariGo.Core.Dto.Request.CategoryItem;
using SafariGo.Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Repositories
{
    public interface ICategoryItemRepositories
    {
        Task<CategoryResponse> GetAllCategoryItem();
        Task<CategoryResponse> AddCategoryItem(CategoryItemRequest request);
        Task<CategoryResponse> DeleteCategoryItem(string id);
        Task<CategoryResponse> UpdateCategoryItem(string id,CategoryItemRequest request);
    }
}
