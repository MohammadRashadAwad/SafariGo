using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafariGo.Core.Dto.Request;
using SafariGo.Core.Repositories;
using System.Security.Claims;

namespace SafariGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAccessController : ControllerBase
    {
        private readonly IAccountAccess _account;

        public AccountAccessController(IAccountAccess account)
        {
            _account = account;
        }
        [Authorize]
        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmailAsync([FromBody]ChangeEmailRequest request)
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _account.ChangeEmailAsync(userId,request);

            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpGet("confirmEmailChange")]
        public async Task<IActionResult> ConfirmEmailChange([FromQuery]ConfirmChangeEmailRequest request)
        {
           
            var result = await _account.ConfirmChangeEmail(request);
            return result.Status ? Redirect("/successfully"): BadRequest(result);
        }
        [Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordRequest request)
        {
          
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result =await _account.ChangePasswordAsync(userId,request);
            return result.Status ? Ok(result) : BadRequest(result);
            
        }

        [Authorize]
        [HttpPost("changePhone")]
        public async Task<IActionResult> ChangePhoneNumberAsync([FromBody]ChangePhoneRequest request)
        {
           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _account.ChangePhoneNumberAsync(userId, request);
            return result.Status ? Ok(result) : BadRequest(result);

        }

        [Authorize]
        [HttpGet("profile/{userId}")]

        public async Task<IActionResult> GetProfile(string userId)
        {
            var result = await _account.ProfileAsync(userId);
            return result.Status ? Ok(result) : BadRequest(result.Message);
        }
    }
}
