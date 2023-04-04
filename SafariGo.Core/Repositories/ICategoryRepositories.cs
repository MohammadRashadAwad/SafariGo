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
        Task<BaseResponse> GetAllCategory();
        Task<BaseResponse> AddCategory(CategoryRequest request);
        Task<BaseResponse> DeleteCategory(string Id);
        Task<BaseResponse> UpdateCategory(string Id ,CategoryRequest request);

    }
}
