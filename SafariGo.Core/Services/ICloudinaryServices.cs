using Microsoft.AspNetCore.Http;
using SafariGo.Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Services
{
    public interface ICloudinaryServices
    {
        Task<BaseResponse> UploadAsync(IFormFile file);
       
        Task<BaseResponse> DeleteResorceAsync(string url);
        Task<BaseResponse> UpdateAsync(string url, IFormFile file);
    }
}
