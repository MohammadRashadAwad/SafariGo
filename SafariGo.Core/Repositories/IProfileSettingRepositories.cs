using SafariGo.Core.Dto.Request;
using SafariGo.Core.Dto.Request.Profile_Setting;
using SafariGo.Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Repositories
{
    public interface IProfileSettingRepositories
    {
        Task<ProfileSettingResponse> UpdateNameAsync(string userId,UpdateNameRequest request);
        Task<ProfileSettingResponse> UpdateBioAsync(string userId,UpdateBioRequest request);
        Task<ProfileSettingResponse> RemoveBioAsync(string userId);
    }
}
