using Emocare.Domain.Entities.Email;
using Emocare.Domain.Interfaces.Repositories.Email;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Emocare.Infrastructure.Repositories.Email
{
    public class OtpRepository : Repository<OtpVerification>,IOtpRepository
    {
        public OtpRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<OtpVerification?> RecentOtp(string email)
        {
            return await _context.OtpVerifications
                .Where(x => x.Email == email && x.ExpirationTime > DateTime.UtcNow && !x.IsUsed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
