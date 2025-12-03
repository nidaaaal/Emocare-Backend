using Emocare.Application.DTOs.Auth;
using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
using Emocare.Shared.Helpers.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _services;
        public AuthenticationController(IAuthenticationServices services)
        {
            _services = services;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto) {

            var res = await _services.Login(dto);
            var token = res?.Data?.Token;
            if (res.Success && token != null) Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7)

            });
            return Ok(res);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(ResponseBuilder.Success("Logged out successfully", "token Removed", "AuthenticationController"));
        }



       [HttpPost("ForgotPassword/ByPrevious")]
        public async Task<IActionResult>ChangeByPrevious (ForgotPasswordDto dto)
            => Ok(await _services.ForgotPasswordRequest(dto));

        [HttpPost("ForgotPassword/ByEmail")]
        public async Task<IActionResult> ChangeByEmail(string email)
           => Ok(await _services.ChangeByVerifiedEmail(email));

        [HttpPost("ForgotPassword/VerifyEmail")]
        public async Task<IActionResult> EmailVerify(string email,string otp)
           => Ok(await _services.VerifyOtp(email,otp));

        [HttpPatch("ForgotPassword/Password")]
        public async Task<IActionResult> ChangePassword(ForgotChangeDto dto)
            => Ok(await _services.ChangeNewPassword(dto.Email,dto.NewPassword));

        [Authorize]
        [HttpPost("signalRToken")]
        public IActionResult GetToken()
            =>Ok(_services.RefreshSignalRToken());
    }
}
