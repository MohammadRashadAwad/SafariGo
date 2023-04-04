using Microsoft.AspNetCore.Http;
using SafariGo.Core.Dto.Request;
using SafariGo.Core.Dto.Request.Profile_Setting;
using SafariGo.Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Repositories
{
    public interface IProfileSettingRepositories
    {
        Task<BaseResponse> UpdateNameAsync(string userId,UpdateNameRequest request);
        Task<BaseResponse> UpdateBioAsync(string userId,UpdateBioRequest request);
        Task<BaseResponse> RemoveBioAsync(string userId);
        Task<BaseResponse> UploadPictureAsync(string userId, IFormFile file, string pictureType);
        Task<BaseResponse> DeletePictureAsync(string userId,string pictureType);

    }
}
