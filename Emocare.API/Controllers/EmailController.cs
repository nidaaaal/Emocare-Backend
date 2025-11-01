using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailServices _emailServices;

        public EmailController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        [HttpPost("otp/request/{email}")]
        public async Task<IActionResult> GenerateOtp(string email) =>  Ok(await _emailServices.SendOtp(email));

        [HttpPost("otp/verify/{email}")]
        public async Task<IActionResult> VerifyOtp(string email,[FromBody]string otp) => Ok(await _emailServices.VerifyOtp(email,otp));

        [HttpGet("check/{email}")]
        public async Task<IActionResult> CheckVerified(string email) => Ok(await _emailServices.CheckVerified(email));

        [HttpPost("Verify/{email}")]
        public async Task<IActionResult> Verify(string email) => Ok(await _emailServices.VerifyEmail(email));
    }
}
