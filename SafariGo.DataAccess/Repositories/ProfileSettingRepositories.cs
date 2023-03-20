using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SafariGo.Core.Dto.Request;
using SafariGo.Core.Dto.Request.Profile_Setting;
using SafariGo.Core.Dto.Response;
using SafariGo.Core.Models;
using SafariGo.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Repositories
{
    public class ProfileSettingRepositories : IProfileSettingRepositories
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileSettingRepositories(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}
