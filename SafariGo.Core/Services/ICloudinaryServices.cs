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
        Task<CloudinaryServiceResponse> UploadAsync(IFormFile file);
       
        Task<CloudinaryServiceResponse> DeleteResorceAsync(string url);
        Task<CloudinaryServiceResponse> UpdateAsync(string url, IFormFile file);
    }
}
