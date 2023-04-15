using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SafariGo.Core.Configure_Services;
using SafariGo.Core.Dto.Request;
using SafariGo.Core.Dto.Request.Profile_Setting;
using SafariGo.Core.Dto.Response;
using SafariGo.Core.Models;
using SafariGo.Core.Repositories;
using SafariGo.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Repositories
{
    public class ProfileSettingRepositories : IProfileSettingRepositories
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICloudinaryServices _cloudinary;

        public ProfileSettingRepositories(UserManager<ApplicationUser> userManager, ICloudinaryServices cloudinary)
        {
            _userManager = userManager;
            _cloudinary = cloudinary;
        }

        public async Task<BaseResponse> DeletePictureAsync(string userId, string pictureType)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BaseResponse { Errors = new { UserId = "Invalid user id" } };
            var pictureUrl = string.Empty;
            if (pictureType == "cover")
                pictureUrl = user.CoverPic;
            else if (pictureType == "profile")
                pictureUrl = user.ProfilePic;
            var delete = await _cloudinary.DeleteResorceAsync(pictureUrl);
            if (!delete.Status)
                return new BaseResponse { Errors = new { Image = delete.Message } };
            if (pictureType == "cover")
                user.CoverPic = null;
            else if (pictureType == "profile")
                user.ProfilePic = null;
            await _userManager.UpdateAsync(user);
            return new BaseResponse
            {
                Status = true,
                Message = delete.Message
            };
        }


        public async Task<BaseResponse> RemoveBioAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BaseResponse { Errors = new { UserId = "Invalid user id" } };
            user.Bio = null;
            await _userManager.UpdateAsync(user);
            return new BaseResponse { Status = true, Data = new { Bio = user.Bio }, Message = "Bio Removed successfully" };
        }

        public async Task<BaseResponse> UpdateBioAsync(string userId, UpdateBioRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BaseResponse { Errors = new { UserId = "Invalid user id" } };
            user.Bio = request.Bio;
            await _userManager.UpdateAsync(user);
            return new BaseResponse { Status = true, Data = new { Bio = user.Bio }, Message = "Bio updated successfully" };
        }

        public async Task<BaseResponse> UpdateNameAsync(string userId, UpdateNameRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BaseResponse { Errors = new { UserId = "Invalid user id" } };
            if (!string.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;
            await _userManager.UpdateAsync(user);

            return new BaseResponse
            {
                Status = true,
                Data = new { Firstname = user.FirstName, Lastname = user.LastName },
                Message = "The name has been updated successfully"
            };

        }





        public async Task<BaseResponse> UploadPictureAsync(string userId, IFormFile file, string pictureType)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new BaseResponse { Errors = new { UserId = "Invalid user id" } };
            }

            var pictureProperty = pictureType == "profile" ? user.ProfilePic : user.CoverPic;
            var uploadResult = string.IsNullOrEmpty(pictureProperty)
                ? await _cloudinary.UploadAsync(file)
                : await _cloudinary.UpdateAsync(pictureProperty, file);

            if (!uploadResult.Status)
            {
                return new BaseResponse { Errors = new { Image = uploadResult.Message } };
            }

            if (pictureType == "profile")
            {
                user.ProfilePic = uploadResult.Data.ToString();
            }
            else if (pictureType == "cover")
            {
                user.CoverPic = uploadResult.Data.ToString();
            }

            await _userManager.UpdateAsync(user);

            return new BaseResponse
            {
                Status = true,
                Message = "The image has been uploaded successfully",
                Data = new { Picture = uploadResult.Data.ToString() }
            };
        }




    }
}
