using Emocare.Domain.Entities.Payment;
using Emocare.Domain.Interfaces.Repositories.Payment;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Emocare.Infrastructure.Repositories.Payment
{
    public class UserPlanRepository:Repository<UserPlan>,IUserPlanRepository
    {
        public UserPlanRepository(AppDbContext context) : base(context) { }

        public async Task<bool> AnyActivePlan(Guid id) => await _context.UserPlans.AnyAsync(x => x.UserId == id && x.EndDate >= DateTime.UtcNow && x.PlanId !=1 && x.IsActive);
        public async Task<UserPlan?> CurrentPlan(Guid id) => await _context.UserPlans.OrderByDescending(x=>x.EndDate).FirstOrDefaultAsync(x => x.UserId == id && x.EndDate >= DateTime.UtcNow && x.IsActive);
    }
}
