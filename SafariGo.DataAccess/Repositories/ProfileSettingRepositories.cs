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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Repositories
{
    public class ProfileSettingRepositories :IProfileSettingRepositories
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICloudinaryServices _cloudinary;

        public ProfileSettingRepositories(UserManager<ApplicationUser> userManager, ICloudinaryServices cloudinary)
        {
            _userManager = userManager;
            _cloudinary = cloudinary;
        }

        public async Task<ProfileSettingResponse> DeleteCoverPicAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ProfileSettingResponse { Errors = new { UserId = "Invalid user id" } };

            var delete = await _cloudinary.DeleteResorceAsync(user.CoverPic);
            if (!delete.Status)
                return new ProfileSettingResponse { Errors = new { Image = delete.Message } };
            user.CoverPic = null;
            await _userManager.UpdateAsync(user);
            return new ProfileSettingResponse
            {
                Status = true,
                Message = delete.Message
            };
        }

        public async Task<ProfileSettingResponse> DeleteProfilePicAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ProfileSettingResponse { Errors = new { UserId = "Invalid user id" } };

            var delete = await _cloudinary.DeleteResorceAsync(user.ProfilePic);
            if (!delete.Status)
                return new ProfileSettingResponse { Errors = new { Image = delete.Message } };
             user.ProfilePic = null ;
            await _userManager.UpdateAsync(user);
            return new ProfileSettingResponse
            {
                Status = true,
                Message = delete.Message
            };
        }

        public async Task<ProfileSettingResponse> RemoveBioAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ProfileSettingResponse { Errors = new { UserId = "Invalid user id" } };
            user.Bio = null;
            await _userManager.UpdateAsync(user);
            return new ProfileSettingResponse { Status = true, Data = new { Bio = user.Bio }, Message = "Bio Removed successfully" };
        }

        public async Task<ProfileSettingResponse> UpdateBioAsync(string userId, UpdateBioRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ProfileSettingResponse { Errors = new { UserId = "Invalid user id" } };
            user.Bio = request.Bio;
            await _userManager.UpdateAsync(user);
            return new ProfileSettingResponse { Status = true, Data = new { Bio = user.Bio }, Message = "Bio updated successfully" };
        }

        public async Task<ProfileSettingResponse> UpdateNameAsync(string userId, UpdateNameRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ProfileSettingResponse { Errors = new { UserId = "Invalid user id" } };

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return new ProfileSettingResponse { Errors = new { Password = "Wrong password" } };
            if (!string.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;
            await _userManager.UpdateAsync(user);

            return new ProfileSettingResponse
            {
                Status = true,
                Data = new { Firstname = user.FirstName, Lastname = user.LastName },
                Message = "The name has been updated successfully"
            };

        }

        public async Task<ProfileSettingResponse> UploadCoverPicAsync(string userId, IFormFile file)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ProfileSettingResponse { Errors = new { UserId = "Invalid user id" } };

            if (string.IsNullOrEmpty(user.CoverPic))
            {
                var upload = await _cloudinary.UploadAsync(file);
                if (!upload.Status)
                    return new ProfileSettingResponse { Errors = new { Image = upload.Message } };
                user.CoverPic = upload.Url;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                var update = await _cloudinary.UpdateAsync(user.CoverPic, file);
                if (!update.Status)
                    return new ProfileSettingResponse { Errors = new { Image = update.Message } };
                user.CoverPic = update.Url;
                await _userManager.UpdateAsync(user);

            }
            return new ProfileSettingResponse
            {
                Status = true,
                Message = "The image has been uploaded successfully",
                Data = new { ProfilePic = user.CoverPic }
            };
        }
    

        public async Task<ProfileSettingResponse> UploadProfilePicAsync(string userId, IFormFile file)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ProfileSettingResponse { Errors = new { UserId = "Invalid user id" } };

            if (string.IsNullOrEmpty(user.ProfilePic))
            {
                var upload = await _cloudinary.UploadAsync(file);
                if (!upload.Status)
                    return new ProfileSettingResponse { Errors = new { Image = upload.Message } };
                user.ProfilePic = upload.Url;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                var update = await _cloudinary.UpdateAsync(user.ProfilePic, file);
                if (!update.Status)
                    return new ProfileSettingResponse { Errors = new { Image = update.Message } };
                user.ProfilePic = update.Url;
                await _userManager.UpdateAsync(user);

            }
            return new ProfileSettingResponse
            {
                Status = true,
                Message = "The image has been uploaded successfully",
                Data =  new { ProfilePic = user.ProfilePic } 
            };
        }
    }
}
