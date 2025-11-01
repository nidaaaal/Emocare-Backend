using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Helper.Common
{
    public interface IEmailHelper
    {
            Task SendEmailAsync(string toEmail, string subject, string plainText, string htmlMessage);

    }
}
