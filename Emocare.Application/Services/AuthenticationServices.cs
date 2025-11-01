using Emocare.Application.DTOs.Auth;
using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Helper.Auth;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IEmailServices _emailServices;
        private readonly IJwtHelper _jwtHelper;
        private readonly IUserFinder _userFind;
        private readonly IPasswordHistoryRepo _historyRepo;
        public AuthenticationServices(IUserRepository userRepository,IPasswordHasher passwordHasher, IJwtHelper jwtHelper,
            IUserFinder userFinder,IPasswordValidator passwordValidator, IEmailServices emailServices,IPasswordHistoryRepo passwordHistory )
        {
            _userFind = userFinder;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
            _passwordValidator = passwordValidator;
            _emailServices = emailServices;
            _historyRepo = passwordHistory;
        }
        public async Task<ApiResponse<AuthResponse>> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email.ToLower());
            if (user == null) return ResponseBuilder.Fail<AuthResponse>("User Not Found", "Login", 404);
            if (!_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            {
                user.FailedLoginAttempts += 1;
                await _userRepository.Update(user);
                if (user.FailedLoginAttempts > 9)
                {
                    user.Status = UserStatus.Locked;
                    await _userRepository.Update(user);
                    return ResponseBuilder.Fail<AuthResponse>(
                        "Your account has been locked due to multiple suspicious login attempts. Please contact support.",
                        "Login",
                        423);
                }
                return ResponseBuilder.Fail<AuthResponse>("Invalid Password ! Try Again", "Login", 401);
            }
            user.FailedLoginAttempts = 0;
            user.LastLogin = DateTime.UtcNow;
            await _userRepository.Update(user);
            string token = _jwtHelper.GetJwtToken(user);
            string signalrToken = _jwtHelper.GenerateShortLivedToken(user.Id);




            var response = new AuthResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                IsUser = user.IsUser,
                IsPsychologist = user.IsPsychologist,
                IsLocked = user.IsLocked,
                Role = user.Role.ToString(),
                Token = token,
                SignalRToken=signalrToken
            };

            return ResponseBuilder.Success(response, "Login Successful", "Login");
        }

        public async Task<ApiResponse<string>> ForgotPasswordRequest(ForgotPasswordDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email);
            if (user == null) return ResponseBuilder.Fail<string>("No UserFound", "ChangePassword", 404);
            var password = await _historyRepo.PreviousPassword(user.Id) ?? throw new BadRequestException("Invalid userId");
            if (!_passwordHasher.VerifyPassword(dto.PreviousPassword, password.PasswordHash))
            {
                user.ChangeAttempt += 1;
                while (user.ChangeAttempt > 6)
                {
                    user.Status = UserStatus.Inactive;
                    return ResponseBuilder.Fail<string>("Your account has been locked due to multiple suspicious change attempts. Please contact support."
                        , "ForgotPassword", 423);
                }
                return ResponseBuilder.Fail<string>("Incorrect Previous Password", "ForgotPassword", 500);
            }
            user.ChangeRequest = true;
            await _userRepository.Update(user);
            return ResponseBuilder.Success("Request Accepted", "User Data Updated");
        }

        public async Task<ApiResponse<string>> ChangeByVerifiedEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null) return ResponseBuilder.Fail<string>("Account Not Found", "userservice", 404);
            await _emailServices.SendOtp(email);

            return ResponseBuilder.Success($"Otp successfully send to {email}", "Otp Send and saved !", "EmailServices");

        }

        public async Task<ApiResponse<string>> VerifyOtp(string email, string otp)
        {
            var user = await _userRepository.GetByEmail(email) ?? throw new BadRequestException("Cant find user");

            var result = await _emailServices.VerifyOtp(email, otp);
            if (result.Success)
            {
                user.ChangeRequest = true;
                return result;

            }
            user.ChangeAttempt += 1;
            return result;
        }

        public async Task<ApiResponse<string>> ChangeNewPassword(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null) return ResponseBuilder.Fail<string>("No UserFound", "ChangePassword", 404);
            if (user.ChangeRequest)
            {
                if (_passwordHasher.VerifyPassword(password, user.PasswordHash)) return ResponseBuilder.Fail<string>("cannot change with older Password", "ChangePassword", 500);
                var previous = await _historyRepo.PreviousPassword(user.Id) ?? throw new BadRequestException("Invalid userId");
                if (!_passwordHasher.VerifyPassword(password, previous.PasswordHash)) return ResponseBuilder.Fail<string>("cannot change with older Password", "ChangePassword", 500);


                var (res, mes) = _passwordValidator.ValidatePassword(password);
                if (!res) return ResponseBuilder.Fail<string>(mes, "ChangePassword", 404);
                var hashed = _passwordHasher.HashPassword(password);
                user.PasswordHash = hashed;
                user.PasswordChangedAt = DateTime.Now;
                var history = new PasswordHistory
                {
                    UserId = user.Id,
                    PasswordHash = hashed,
                };
                await _historyRepo.Add(history);
                await _userRepository.Update(user);


                return ResponseBuilder.Success("Password Changed successfully", "User Data updated", "ChangePassword");
            }
            return ResponseBuilder.Fail<string>("No Access to change password.Try Again!", "ChangePassword", 404);
        }


        public ApiResponse<string> RefreshSignalRToken()
        {   
            Guid userId = _userFind.GetId();

            string token = _jwtHelper.GenerateShortLivedToken(userId) ?? throw new UnauthorizedAccessException("No access Found");

            return ResponseBuilder.Success(token, "new token returned", "RefreshSignalRToken");
        }

    }
}
