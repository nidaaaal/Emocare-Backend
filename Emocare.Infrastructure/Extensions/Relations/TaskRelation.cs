using Emocare.Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emocare.Infrastructure.Extensions.Relations
{
    public class TaskRelation : IEntityTypeConfiguration<UserDailyTask>
    {
        public void Configure(EntityTypeBuilder<UserDailyTask> entity)
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.WellnessTask)
                  .WithMany(x => x.UserDailyTasks)
                  .HasForeignKey(x => x.WellnessTaskId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.User)
                  .WithMany(u => u.UserDailyTasks)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Optional: composite index to prevent duplicate tasks per user per day
            entity.HasIndex(x => new { x.UserId, x.WellnessTaskId, x.DateAssigned }).IsUnique();
        }
    }


    public class StreakRelation : IEntityTypeConfiguration<UserStreak>
    {
        public void Configure(EntityTypeBuilder<UserStreak> entity)
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.Users)
                  .WithMany(u => u.UserStreaks)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class PsychologistTaskAssignmentRelation : IEntityTypeConfiguration<PsychologistTaskAssignment>
    {
        public void Configure(EntityTypeBuilder<PsychologistTaskAssignment> entity)
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.Psychologist)
                  .WithMany(u => u.AssignedTasks)
                  .HasForeignKey(x => x.PsychologistId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Client)
                  .WithMany(u => u.PsychologistAssignments)
                  .HasForeignKey(x => x.ClientId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.WellnessTask)
                  .WithMany(wt => wt.PsychologistTaskAssignments)
                  .HasForeignKey(x => x.WellnessTaskId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class UserDailyTaskByPsychologistRelation : IEntityTypeConfiguration<UserDailyTaskByPsychologist>
    {
        public void Configure(EntityTypeBuilder<UserDailyTaskByPsychologist> entity)
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.PsychologistTaskAssignment)
                  .WithMany(p => p.UserDailyTasks)
                  .HasForeignKey(x => x.PsychologistTaskAssignmentId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }




}
