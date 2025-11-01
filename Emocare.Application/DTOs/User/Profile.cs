

using Emocare.Domain.Entities.Auth;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Emocare.Application.DTOs.User
{
    #region request
    public class UpdateProfileDto
    {
        [Required, MinLength(3), MaxLength(15)]
        public string FullName { get; set; } = string.Empty;

        [Required, Range(13, 99)]
        public int Age { get; set; }

        [Required, MaxLength(6)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Job { get; set; } = string.Empty;

        [Required]
        public string RelationshipStatus { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

    }

    public class UpdateProfilePicture
    {
        public Guid Id { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

    #endregion


    #region response

    public class UserProfileDto
    {
        public Guid Id { get; set; }

        [Required, MinLength(3), MaxLength(30)]
        public string FullName { get; set; } = string.Empty;

        [Required, Range(13, 99)]
        public int? Age { get; set; }

        [Required, MaxLength(6)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Job { get; set; } = string.Empty;

        [Required]
        public string RelationshipStatus { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

        [Url]
        public string? ProfileImageUrl { get; set; }
    }

    public class GetPsychologistDto
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string? Country { get; set; }
        public string? City { get; set; }
        public string EmailAddress { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Specialization { get; set; } = null!;

        [Required, MaxLength(50)]
        public string LicenseNumber { get; set; } = null!;

        [Range(0, 60)]
        public int? Experience { get; set; }

        [MaxLength(1000)]
        public string? Biography { get; set; }

        [MaxLength(200)]
        public string? Education { get; set; }

    }
    #endregion

}
