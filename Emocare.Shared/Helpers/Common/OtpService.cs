using Emocare.Domain.Interfaces.Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Shared.Helpers.Common
{
    public class OtpService:IOtpService
    {
        public string GenerateOtp()
        {
            var rng = new Random();
            return rng.Next(100000, 999999).ToString();
        }

    }
}
