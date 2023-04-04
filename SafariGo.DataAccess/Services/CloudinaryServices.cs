using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using SafariGo.Core.Configure_Services;
using SafariGo.Core.Dto.Response;
using SafariGo.Core.Extensions;
using SafariGo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Services
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _settings;

        public CloudinaryServices(IOptions<CloudinarySettings> settings)
        {
            _settings = settings.Value;
            _cloudinary = new Cloudinary(new Account(_settings.CloudName, _settings.APIKey, _settings.APISecret));

        }

        public async Task<BaseResponse> DeleteResorceAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                return new BaseResponse { Message = "The URL is Null or Empty" };
            var deleteResult = await _cloudinary.DeleteResourcesAsync(url.ExtractPublicIdOfImage());
            return new BaseResponse { Status = true, Message = "The image has been deleted successfully" };

        }

        public async Task<BaseResponse> UpdateAsync(string url, IFormFile file)
        {
            var delete = await DeleteResorceAsync(url);
            if (!delete.Status)
                return new BaseResponse { Message = delete.Message };
            var upload = await UploadAsync(file);
            if (!upload.Status)
                return new BaseResponse { Message = upload.Message };

            return new BaseResponse
            {
                Status = true,
                Message = "The image has been updated successfully",
                Data = upload.Data
            };
        }

        public async Task<BaseResponse> UploadAsync(IFormFile file)
        {
            if (file.Length == 0 || file == null)
                return new BaseResponse { Message = "The File is requerd" };


            if (!file.ContentType.StartsWith("image/"))
                return new BaseResponse { Message = "We do not support this type of file" };

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return new BaseResponse
            {
                Status = true,
                Message = "The image has been uploaded successfully",
                Data = uploadResult.SecureUrl.ToString()
            };

        }


    }
}
