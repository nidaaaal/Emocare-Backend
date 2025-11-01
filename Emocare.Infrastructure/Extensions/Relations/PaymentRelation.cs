using Emocare.Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emocare.Infrastructure.Extensions.Relations
{
    public class PlanRelation:IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> entity)
        {
            entity.HasMany(x=>x.UserPlans).WithOne(x=>x.Plan).HasForeignKey(x=>x.PlanId).OnDelete(DeleteBehavior.Restrict);
            entity.Property(x => x.Price).IsRequired().HasColumnType("decimal(8,2)");

            entity.HasData(
                new Plan { Id=1,Name="Free",Duration=0,Price=0,IsDelete=false},
                new Plan { Id = 2, Name = "Base", Duration = 15, Price = 99, IsDelete = false }
                );
        }
    }

    public class UserPlanRelation : IEntityTypeConfiguration<UserPlan>
    {
        public void Configure(EntityTypeBuilder<UserPlan> entity)
        {
            entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
