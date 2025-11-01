

using Emocare.Application.DTOs.Auth;
using Emocare.Application.DTOs.User;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface IAuthenticationServices
    {
        Task <ApiResponse<AuthResponse>> Login(LoginDto dto);
        Task<ApiResponse<string>> ForgotPasswordRequest(ForgotPasswordDto dto);
        Task<ApiResponse<string>> ChangeNewPassword(string email, string password);
        Task<ApiResponse<string>> ChangeByVerifiedEmail(string email);
        Task<ApiResponse<string>> VerifyOtp(string email, string otp);
        ApiResponse<string> RefreshSignalRToken();
    }
}
