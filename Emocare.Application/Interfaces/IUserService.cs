using Emocare.Application.DTOs.User;
using Emocare.Domain.Entities.Auth;
using Emocare.Shared.Helpers.Api;
using Microsoft.AspNetCore.Http;


namespace Emocare.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<string>> UserRegister(UserRegisterDto dto);
        Task<ApiResponse<string>> ChangePassword(PasswordChangeDto dto);
        Task<ApiResponse<UserProfileDto>?> ViewProfile();
        Task<ApiResponse<UserProfileDto>?> UpdateProfile(UpdateProfileDto dto);
        Task<ApiResponse<string>?> UpdateProfilePicture(IFormFile file);
        Task<ApiResponse<IEnumerable<Users>>> GetAllDetails();
        Task<ApiResponse<string>> BanUser(Guid userId);


    }
}
