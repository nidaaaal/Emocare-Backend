using Emocare.Application.DTOs.Common;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Email;
using Emocare.Domain.Interfaces.Helper.Common;
using Emocare.Domain.Interfaces.Repositories.Email;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Shared.Helpers.Api;
using Microsoft.AspNetCore.OutputCaching;

namespace Emocare.Application.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IEmailHelper _emailHelp;
        private readonly IOtpRepository _otpRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IOtpService _otpService;
        private readonly IRazorViewToStringRenderer _razorToStringRenderer;

        public EmailServices(IEmailHelper emailHelper, IUserRepository userRepository,IEmailRepository emailRepository,
            IOtpRepository otpRepository, IRazorViewToStringRenderer razorViewToString, IOtpService otpService)
        {
            _emailHelp = emailHelper;
            _otpRepository = otpRepository;
            _razorToStringRenderer = razorViewToString;
            _otpService = otpService;
            _emailRepository = emailRepository;
        }

        public async Task<ApiResponse<string>> SendOtp(string email)
        {
            var otp = _otpService.GenerateOtp();
            string name = email.Split('@')[0];
            var model = new OtpEmailDto
            {
                Name = name,
                Otp = otp
            };

            var otpRepo = new OtpVerification
            {
                 Email= email,
                CreatedAt = DateTime.UtcNow,
                OtpCode = otp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            string plainText = $"Hi,\n\nYour OTP code is: {otp}\n\nThis code is valid for 5 minutes.\n\nThanks,\nEmocare Team";
            string html = await _razorToStringRenderer.RenderViewToStringAsync("OtpEmail", model);

            await _emailHelp.SendEmailAsync(email, "Your OTP Code", plainText, html);

            await _otpRepository.Add(otpRepo);
            return ResponseBuilder.Success($"Otp successfully send to {email}", "Otp Send and saved !", "EmailServices");

        }


        public async Task<ApiResponse<string>> VerifyOtp(string email, string otp)
        {
            var recent = await _otpRepository.RecentOtp(email);

            if (recent == null)
                return ResponseBuilder.Fail<string>("Request for new OTP", "EmailServices", 404);

            if (recent.ExpirationTime < DateTime.UtcNow)
                return ResponseBuilder.Fail<string>("OTP Expired! Request for new OTP", "EmailServices", 404);

            if (recent.OtpCode != otp)
                return ResponseBuilder.Fail<string>("Invalid OTP", "EmailServices", 404);

            return ResponseBuilder.Success("OTP Verified", "OTP Verified Successfully", "EmailServices");
        }


        public async Task<bool> CheckVerified(string email)
        {
            return await _emailRepository.Verified(email);
        }
        public async Task<ApiResponse<string>> VerifyEmail(string email)
        {
            var data = new VerifiedEmail
            {
                IsVerified = true,
                Email = email,
                VerifiedOn = DateTime.UtcNow
            };
         
            await _emailRepository.Update(data);
            return ResponseBuilder.Success("Your Email Verified Successfully", "Email Updated", "EmailServices");

        }

    }
}
