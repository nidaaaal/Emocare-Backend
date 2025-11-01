using Emocare.Domain.Entities.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Repositories.Email
{
    public interface IOtpRepository:IRepository<OtpVerification>
    {
        Task<OtpVerification?> RecentOtp(string email);
    }
}
