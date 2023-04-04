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
        Task<BaseResponse> GetAllCategoryItem();
        Task<BaseResponse> AddCategoryItem(CategoryItemRequest request);
        Task<BaseResponse> DeleteCategoryItem(string id);
        Task<BaseResponse> UpdateCategoryItem(string id,CategoryItemRequest request);
    }
}
