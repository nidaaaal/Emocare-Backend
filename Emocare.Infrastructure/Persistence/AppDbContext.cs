using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Email;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Entities.Journal;
using Emocare.Domain.Entities.Payment;
using Emocare.Domain.Entities.Private;
using Emocare.Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Emocare.Infrastructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; } 
        public DbSet<PsychologistProfile> PsychologistProfiles { get; set; }
        public DbSet<PasswordHistory> PasswordHistory { get; set; } 
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<WellnessTask> WellnessTasks { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCategory> HabitCategories { get; set; }   
        public DbSet<HabitCompletion> HabitCompletion { get; set; }
        public DbSet<UserPushSubscription> UserPushSubscriptions { get; set; }
        public DbSet<OtpVerification> OtpVerifications { get; set; }
        public DbSet<VerifiedEmail> VerifiedEmail { get; set; }
        public DbSet<UserChatMessage> UserChatMessages { get; set; }
        public DbSet<UserChatSession> UserChatSessions { get; set; }
        public DbSet<ChatParticipant> ChatParticipants { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<UserPlan> UserPlans { get; set; }
        public DbSet<UserDailyTask> UserDailyTasks { get; set; }    
        public DbSet<UserStreak> UserStreaks { get; set; }
        public DbSet<PsychologistTaskAssignment> PsychologistTaskAssignments { get; set; }
        public DbSet<UserDailyTaskByPsychologist> UserDailyTaskByPsychologists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
