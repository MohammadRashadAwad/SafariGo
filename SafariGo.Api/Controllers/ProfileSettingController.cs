using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafariGo.Core.Dto.Request;
using SafariGo.Core.Dto.Request.Profile_Setting;
using SafariGo.Core.Repositories;
using System.Security.Claims;

namespace SafariGo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileSettingController : ControllerBase
    {
        private readonly IProfileSettingRepositories _profile;

        public ProfileSettingController(IProfileSettingRepositories profile)
        {
            _profile = profile;
        }

        [HttpPut("updateName")]

        public async Task<IActionResult> UpdateNameAsync(UpdateNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profile.UpdateNameAsync(userId,request);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpPut("updateBio")]
        public async Task<IActionResult> UpdateBioAsync(UpdateBioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profile.UpdateBioAsync(userId, request);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("removeBio")]
        public async Task<IActionResult> RemoveBioAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profile.RemoveBioAsync(userId);
            return result.Status ? Ok(result) : BadRequest(result);
        }



    }
}
