using SafariGo.Core.Dto.Request.Category;
using SafariGo.Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Repositories
{
    public interface ICategoryRepositories
    {
        Task<CategoryResponse> GetAllCategory();
        Task<CategoryResponse> AddCategory(CategoryRequest request);
        Task<CategoryResponse> DeleteCategory(string Id);
        Task<CategoryResponse> UpdateCategory(string Id ,CategoryRequest request);

    }
}
