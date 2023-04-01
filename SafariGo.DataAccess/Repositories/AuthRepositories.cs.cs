
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SafariGo.Core.Configure_Services;
using SafariGo.Core.Dto.Request.Authentication;
using SafariGo.Core.Dto.Response;
using SafariGo.Core.Models;
using SafariGo.Core.Repositories;
using SafariGo.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Repositories
{
    public class AuthRepositories : IAuthRepositories
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly IConfiguration _configuration;
        private readonly IMaillingService _maillingService;


        public AuthRepositories(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt, IConfiguration configuration, IMaillingService maillingService)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _configuration = configuration;
            _maillingService = maillingService;
        }

        public async Task<AuthResponse> ConfirmEmail(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new AuthResponse { Errors = new { UserId = "Invalid User Id " } };
            var decoded = WebEncoders.Base64UrlDecode(request.Token);
            var token = Encoding.UTF8.GetString(decoded);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return new AuthResponse { Errors = new { confirm = result.Errors.Select(e => e.Description) } };
            return new AuthResponse { Status = true };

        }

        public async Task<AuthResponse> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new AuthResponse { Errors = new { Email = "There is no user associated with this e-mail" } };
           var GenerateResetPasswordToken= await _userManager.GeneratePasswordResetTokenAsync(user);
            var encoding = Encoding.UTF8.GetBytes(GenerateResetPasswordToken);
            var validToken = WebEncoders.Base64UrlEncode(encoding);
            var url = $"{_configuration["AppUrl"]}/ResetPassword?email={user.Email}&token={validToken}";
            await _maillingService.ResetPasswordAsync(user.Email,user.FirstName,url);
            
            return new AuthResponse { Status = true };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            // check username Associated with user
            var user = new EmailAddressAttribute().IsValid(request.Username) ?
                await _userManager.FindByEmailAsync(request.Username) :
                await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return new AuthResponse { Errors = new { Username = "We couldn't find an account with this email or phone number. " } };
            if (!user.EmailConfirmed)
                return new AuthResponse { Errors = new { Username = "Please verify your email before logging in" } };
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return new AuthResponse { Errors = new { Password = "Invalid Username or Password" } };
            return new AuthResponse
            {
                Status = true,
                Data = new Data
                {
                    IsAdmin = await _userManager.IsInRoleAsync(user, "Admin"),
                    Token = new JwtSecurityTokenHandler().WriteToken(await CreateAccessToken(user)),
                    UserId = user.Id
                },
                Errors = null
            };

        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // check the email is not exists in Database
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return new AuthResponse { Errors = new { Email = "Someone already has this email address. Try another email" } };
            // check the phone is not exists in Database
            if (await _userManager.FindByNameAsync(request.Phone) != null)
                return new AuthResponse { Errors = new { Phone = "Someone already has this phone number. Try another phone number" } };
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.Phone,
                UserName = request.Phone
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new AuthResponse { Errors = new { Password = result.Errors.Select(e => e.Description) } };

            await _userManager.AddToRoleAsync(user, "User");
            // Generate Token to confirm email 
            var tokenConfirmEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // create Token not contains special characters
            var encodedToken = Encoding.UTF8.GetBytes(tokenConfirmEmail);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            var url = $"{_configuration["AppUrl"]}/api/Authentication/confirmEmail?userId={user.Id}&token={validToken}";
            await _maillingService.ConfirmEamilAsync(user.Email, user.FirstName, url);
            return new AuthResponse
            {
                Status = true,
                Data = new Data
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(await CreateAccessToken(user)),
                    UserId = user.Id,
                    IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
                },
                Errors = null,

            };



        }

        public async Task<AuthResponse> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new AuthResponse { Errors = new { Email = "There is no user associated with this e-mail" } };
            var decoded = WebEncoders.Base64UrlDecode(request.Token);
            var validToken = Encoding.UTF8.GetString(decoded);
            var result = await _userManager.ResetPasswordAsync(user, validToken, request.Password);
            if (!result.Succeeded)
                return new AuthResponse { Errors = new { Password = result.Errors.Select(e => e.Description) } };
            return new AuthResponse { Status = true };

            
        }


        #region CreatAccessToken
        private async Task<JwtSecurityToken> CreateAccessToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user); // null
            var roles = await _userManager.GetRolesAsync(user); // User
            var roleClaims = new List<Claim>();  // {roles :user}

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id), //userid
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //ddmsdk-deffe-d1e-
                new Claim(JwtRegisteredClaimNames.Email, user.Email), //email
                new Claim("UserName", user.UserName)  //username
            }
            .Union(userClaims)
            .Union(roleClaims);
            // payload username jwt id email user id roles

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDay),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        #endregion

    }
}
