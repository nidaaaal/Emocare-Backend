using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Emocare.Infrastructure.Repositories.User
{
    public class PsychologistRepository : Repository<PsychologistProfile>,IPsychologistRepository
    {
        public PsychologistRepository(AppDbContext appContext) :base(appContext) { }

        public async Task<IEnumerable<PsychologistProfile>> GetAllActive() => await _dbSet.Include(x=>x.Users).Where(x=>x.IsApproved).ToListAsync();
    }
}
