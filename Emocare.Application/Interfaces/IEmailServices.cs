using Emocare.Shared.Helpers.Api;


namespace Emocare.Application.Interfaces
{
    public interface IEmailServices
    {
        Task <ApiResponse<string>> SendOtp(string email);
        Task<ApiResponse<string>> VerifyOtp(string email, string otp);
        Task<bool> CheckVerified(string email);
        Task<ApiResponse<string>> VerifyEmail(string email);

    }
}
