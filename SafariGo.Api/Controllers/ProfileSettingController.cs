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

        public async Task<IActionResult> UpdateNameAsync([FromBody]UpdateNameRequest request)
        {
           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profile.UpdateNameAsync(userId,request);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpPut("updateBio")]
        public async Task<IActionResult> UpdateBioAsync(UpdateBioRequest request)
        {
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

       

        [HttpPut("uploadProfilePic")]

        public async Task<IActionResult> UploadProfilePic(IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profile.UploadPictureAsync(userId, file, "profile");
            return result.Status ? Ok(result) : BadRequest(result);

        }

        [HttpDelete("DeleteProfilePic")]

        public async Task<IActionResult> DeleteProfilePic()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profile.DeletePictureAsync(userId, "profile");
            return result.Status ? Ok(result) : BadRequest(result);

        }


        [HttpPut("uploadCoverPic")]

        public async Task<IActionResult> UploadCoverPic(IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profile.UploadPictureAsync(userId, file, "cover");
            return result.Status ? Ok(result) : BadRequest(result);

        }

        [HttpDelete("DeleteCoverPic")]

        public async Task<IActionResult> DeleteCoverPic()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profile.DeletePictureAsync(userId, "cover");
            return result.Status ? Ok(result) : BadRequest(result);

        }


    }
}
