using SafariGo.Core.Dto.Request;
using SafariGo.Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Repositories
{
    public interface IAccountAccess
    {
        Task<BaseResponse> ChangeEmailAsync(string userId, ChangeEmailRequest request);
        Task<BaseResponse> ConfirmChangeEmail(ConfirmChangeEmailRequest request);
        Task<BaseResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        Task<BaseResponse> ChangePhoneNumberAsync(string userId,ChangePhoneRequest request);
        Task<BaseResponse> ProfileAsync(string userId);
    }
}
