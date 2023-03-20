using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SafariGo.Core.Dto.Request;
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
    public class AccountAccess : IAccountAccess
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMaillingService _maillingService;

        public AccountAccess(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMaillingService maillingService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _maillingService = maillingService;
        }

        public async Task<AccountAccessResponse> ChangeEmailAsync(string userId, ChangeEmailRequest request)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (userId == null)
                return new AccountAccessResponse { Errors = new { UserId = "Invalid User Id" }, Message = "Email change failed" };
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return new AccountAccessResponse { Errors = new { Email = "The email already exists" }, Message = "Email change failed" };
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return new AccountAccessResponse { Errors = new { Password = "The password is incorrect" }, Message = "Email change failed" };
            var GenerateChangeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(user, request.Email);
            var encoding = Encoding.UTF8.GetBytes(GenerateChangeEmailToken);
            var validToken = WebEncoders.Base64UrlEncode(encoding);
            var url = $"{_configuration["AppUrl"]}/api/AccountAccess/confirmEmailChange?userId={user.Id}&token={validToken}&email={request.Email}";
            await _maillingService.ConfirmEamilAsync(request.Email, user.FirstName, url);

            return new AccountAccessResponse { Status = true, Message = "Check your email" };

            throw new NotImplementedException();
        }

        public async Task<AccountAccessResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new AccountAccessResponse { Errors = new { UserId = "Invalid User Id" }, Message = "Password change failed" };
            if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
                return new AccountAccessResponse { Errors = new { CurrentPassword = "The password is incorrect" } };
            var result = await _userManager.ChangePasswordAsync(user,request.CurrentPassword,request.NewPassword);
            if (!result.Succeeded)
                return new AccountAccessResponse { Errors = new { NewPassword = result.Errors.Select(e => e.Description) } };
            return new AccountAccessResponse { Status = true, Message = "The password has been changed successfully" };
            
        }

        public async Task<AccountAccessResponse> ChangePhoneNumberAsync(string userId, ChangePhoneRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
                return new AccountAccessResponse { Errors = new { UserId = "Invalid User Id" }, Message = "Phone Number change failed" };
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return new AccountAccessResponse { Errors = new { CurrentPassword = "The password is incorrect" } };
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.PhoneNumber;
            user.NormalizedUserName = request.PhoneNumber;
          var result =  await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new AccountAccessResponse { Errors = new { PhoneNumber = result.Errors.Select(e => e.Description) } };
            return new AccountAccessResponse { Status = true, Message = "The phone number  has been changed successfully" };
        }

        public async Task<AccountAccessResponse> ConfirmChangeEmail(ConfirmChangeEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new AccountAccessResponse { Errors = new { UserId = "Invalid User Id " } };
            var decoded = WebEncoders.Base64UrlDecode(request.Token);
            var validToken = Encoding.UTF8.GetString(decoded);
            var result = await _userManager.ChangeEmailAsync(user, request.Email, validToken);
            if (!result.Succeeded)
                return new AccountAccessResponse { Errors = new { confirm = result.Errors.Select(e => e.Description) } };
            return new AccountAccessResponse { Status = true, Message = "Email changed successfully" };

        }
    }
}
