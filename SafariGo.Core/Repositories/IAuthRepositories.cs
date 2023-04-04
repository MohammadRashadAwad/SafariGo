using SafariGo.Core.Dto.Request.Authentication;
using SafariGo.Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Repositories
{
    public interface IAuthRepositories
    {
        Task<BaseResponse> RegisterAsync(RegisterRequest request);
        Task<BaseResponse> LoginAsync(LoginRequest request);
        Task<BaseResponse> ConfirmEmail(ConfirmEmailRequest request);
        Task<BaseResponse> ForgetPassword(string email);
        Task<BaseResponse> ResetPassword(ResetPasswordRequest request);
    }
}
