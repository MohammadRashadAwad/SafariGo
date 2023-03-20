using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Services
{
    public interface IMaillingService
    {
        Task SendEmailAsync(string mailTo, string subject, string body);
        Task ConfirmEamilAsync(string mailTo ,string userName,string url);
        Task ResetPasswordAsync(string mailTo ,string userName,string url);
    }
}
