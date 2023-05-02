using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
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
        const string defaultProfilePicture = "https://cdn-icons-png.flaticon.com/512/847/847969.png?w=740&t=st=1683038294~exp=1683038894~hmac=ff15f90aa346ed552d76f40cc8c100ccc3405550ac77655202761ba2dcaf3865";
        const string defaultCoverPicture = "https://okaystartup.com/images/home4.jpg";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMaillingService _maillingService;
        private readonly ApplicationDbContext _context;

        public AccountAccess(UserManager<ApplicationUser> userManager, IConfiguration configuration,
            IMaillingService maillingService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _maillingService = maillingService;
            _context = context;
        }

        public async Task<BaseResponse> ChangeEmailAsync(string userId, ChangeEmailRequest request)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (userId == null)
                return new BaseResponse { Errors = new { UserId = "Invalid User Id" }, Message = "Email change failed" };
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return new BaseResponse { Errors = new { Email = "The email already exists" }, Message = "Email change failed" };
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return new BaseResponse { Errors = new { Password = "The password is incorrect" }, Message = "Email change failed" };
            var GenerateChangeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(user, request.Email);
            var encoding = Encoding.UTF8.GetBytes(GenerateChangeEmailToken);
            var validToken = WebEncoders.Base64UrlEncode(encoding);
            var url = $"{_configuration["AppUrl"]}/api/AccountAccess/confirmEmailChange?userId={user.Id}&token={validToken}&email={request.Email}";
            await _maillingService.ConfirmEamilAsync(request.Email, user.FirstName, url);

            return new BaseResponse { Status = true, Message = "Check your email" };

            throw new NotImplementedException();
        }

        public async Task<BaseResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BaseResponse { Errors = new { UserId = "Invalid User Id" }, Message = "Password change failed" };
            if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
                return new BaseResponse { Errors = new { CurrentPassword = "The password is incorrect" } };
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return new BaseResponse { Errors = new { NewPassword = result.Errors.Select(e => e.Description) } };
            return new BaseResponse { Status = true, Message = "The password has been changed successfully" };

        }

        public async Task<BaseResponse> ChangePhoneNumberAsync(string userId, ChangePhoneRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BaseResponse { Errors = new { UserId = "Invalid User Id" }, Message = "Phone Number change failed" };
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return new BaseResponse { Errors = new { CurrentPassword = "The password is incorrect" } };
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.PhoneNumber;
            user.NormalizedUserName = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new BaseResponse { Errors = new { PhoneNumber = result.Errors.Select(e => e.Description) } };
            return new BaseResponse { Status = true, Message = "The phone number  has been changed successfully" };
        }

        public async Task<BaseResponse> ConfirmChangeEmail(ConfirmChangeEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new BaseResponse { Errors = new { UserId = "Invalid User Id " } };
            var decoded = WebEncoders.Base64UrlDecode(request.Token);
            var validToken = Encoding.UTF8.GetString(decoded);
            var result = await _userManager.ChangeEmailAsync(user, request.Email, validToken);
            if (!result.Succeeded)
                return new BaseResponse { Errors = new { confirm = result.Errors.Select(e => e.Description) } };
            return new BaseResponse { Status = true, Message = "Email changed successfully" };

        }

        public async Task<BaseResponse> ProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BaseResponse { Message = "User Not Found" };
            var userInfo = await _context.Users
                .Include(p => p.Posts)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .Select(u => new
                {
                    UserId = u.Id,
                    email = u.Email,
                    phone = u.PhoneNumber,
                    name = u.FirstName + " " + u.LastName,
                    profilepicture = u.ProfilePic ?? defaultProfilePicture,
                    coverpicture = u.CoverPic ?? defaultCoverPicture,
                    bio = u.Bio,
                    numberofpost = u.Posts.Count,

                    Posts = u.Posts.Select(p => new
                    {
                        PostId = p.Id,
                        Description = p.description,
                        Poster = p.Poster,
                        PostCreated = p.CreateAt,
                        NumberOfComments = p.Comments.Count,

                        Comments = p.Comments.Select(c => new
                        {
                            CommentId = c.Id,
                            CommenterId = c.UserId,
                            CommenterName = c.Users.FirstName + " " + c.Users.LastName,
                            CommenterPic = c.Users.ProfilePic ?? defaultProfilePicture,
                            DescriptionComment = c.description,
                            Image = c.Image,
                            CommentCreated = c.CrateAt

                        }),
                        NumberOfLikes = p.Likes.Count,
                        Likes = p.Likes.Select(l => new
                        {
                            LikeId = l.LikeId,
                            UserId = l.UserId,
                            Name = l.Users.FirstName + " " + l.Users.LastName,
                            LikedPic = l.Users.ProfilePic
                        })
                    })

                }).FirstOrDefaultAsync(u => u.UserId == userId);

            return new BaseResponse
            {
                Status=true,
                Data = userInfo
            };
        }
    }
}
