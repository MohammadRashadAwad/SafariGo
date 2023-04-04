
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafariGo.Core.Dto.Request.Authentication;
using SafariGo.Core.Dto.Response;
using SafariGo.Core.Repositories;
using SafariGo.Core.Services;

namespace SafariGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepositories _auth;

        public AuthenticationController(IAuthRepositories auth, IMaillingService mailling)
        {
            _auth = auth;
        }

        [HttpPost("register")]

        public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequest request)
        {
            var result = await _auth.RegisterAsync(request);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequest request)
        {
           
            var result = await _auth.LoginAsync(request);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpGet("confirmEmail")]

        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            
            var result = await _auth.ConfirmEmail(request);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery]string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("The email address is requerd");
            var resut = await _auth.ForgetPassword(email);
            return resut.Status ? Ok(resut) : BadRequest(resut);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromForm]ResetPasswordRequest request)
        {
           
            var result = await _auth.ResetPassword(request);

            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
