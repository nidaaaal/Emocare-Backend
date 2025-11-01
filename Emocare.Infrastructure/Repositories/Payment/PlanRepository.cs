using Emocare.Domain.Entities.Payment;
using Emocare.Domain.Interfaces.Repositories.Payment;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Emocare.Infrastructure.Repositories.Payment
{
    public class PlanRepository:Repository<Plan>,IPlanRepository
    {
        public PlanRepository(AppDbContext context) : base(context) { }

        public async Task<Plan?> GetById(int id) => await _dbSet.FindAsync(id);
        public async Task<bool> GetByName(string name)=> await _dbSet.AnyAsync(x=>x.Name == name);
        public async Task<bool> GetByPrice(decimal price) => await _dbSet.AnyAsync(x => x.Price == price);

    }
}
