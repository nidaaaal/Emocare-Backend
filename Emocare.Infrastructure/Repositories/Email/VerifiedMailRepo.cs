

using Emocare.Domain.Entities.Email;
using Emocare.Domain.Interfaces.Repositories.Email;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Emocare.Infrastructure.Repositories.Email
{
    public class EmailRepository : Repository<VerifiedEmail>, IEmailRepository
    {
        public EmailRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<bool> Verified(string email)
            => await _dbSet.AnyAsync(x=>x.Email==email && x.IsVerified);

        public async Task<VerifiedEmail?> GetByEmail(string email)
            => await _dbSet.FirstOrDefaultAsync(x => x.Email == email);

    }
}
