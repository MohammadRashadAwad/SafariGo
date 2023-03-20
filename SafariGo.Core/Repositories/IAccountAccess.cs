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
        Task<AccountAccessResponse> ChangeEmailAsync(string userId, ChangeEmailRequest request);
        Task<AccountAccessResponse> ConfirmChangeEmail(ConfirmChangeEmailRequest request);
        Task<AccountAccessResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        Task<AccountAccessResponse> ChangePhoneNumberAsync(string userId,ChangePhoneRequest request);
    }
}
