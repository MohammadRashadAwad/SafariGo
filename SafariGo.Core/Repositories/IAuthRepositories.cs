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
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> ConfirmEmail(ConfirmEmailRequest request);
        Task<AuthResponse> ForgetPassword(string email);
        Task<AuthResponse> ResetPassword(ResetPasswordRequest request);
    }
}
