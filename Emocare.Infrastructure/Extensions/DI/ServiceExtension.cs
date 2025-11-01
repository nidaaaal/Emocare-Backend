using Emocare.Application.Interfaces;
using Emocare.Application.Mappings;
using Emocare.Application.Services;
using Emocare.Domain.Interfaces.Extension;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Helper.Auth;
using Emocare.Domain.Interfaces.Helper.Common;
using Emocare.Domain.Interfaces.Repositories;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Domain.Interfaces.Repositories.Email;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Domain.Interfaces.Repositories.Payment;
using Emocare.Domain.Interfaces.Repositories.Private;
using Emocare.Domain.Interfaces.Repositories.Tasks;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Infrastructure.Extensions.Integrations.CloudinaryExtension;
using Emocare.Infrastructure.Persistence;
using Emocare.Infrastructure.Repositories;
using Emocare.Infrastructure.Repositories.Chat;
using Emocare.Infrastructure.Repositories.Email;
using Emocare.Infrastructure.Repositories.Habits;
using Emocare.Infrastructure.Repositories.Payment;
using Emocare.Infrastructure.Repositories.Private;
using Emocare.Infrastructure.Repositories.Tasks;
using Emocare.Infrastructure.Repositories.User;
using Emocare.Shared.Helpers.Auth;
using Emocare.Shared.Helpers.Chat;
using Emocare.Shared.Helpers.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emocare.Infrastructure.Extensions.DI
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            sql => sql.EnableRetryOnFailure()
    ));

        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));    
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IPsychologistRepository, PsychologistRepository>();
            services.AddScoped<IPasswordHistoryRepo, PasswordHistoryRepo>();
            services.AddScoped<IWellnessTaskRepository, WellnessTaskRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatSessionRepository,ChatSessionRepository>();
            services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
            services.AddScoped<IHabitRepository,HabitRepository>();
            services.AddScoped<IHabitHabitCompletionRepository, HabitHabitCompletionRepository>();
            services.AddScoped<IHabitCategoryRepository, HabitCategoryRepository>();    
            services.AddScoped<INotificationSubscriptionRepository, NotificationSubscriptionRepository>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IPlanRepository,PlanRepository>();
            services.AddScoped<IUserPlanRepository, UserPlanRepository>();
            services.AddScoped<IUserChatMessageRepository, UserChatMessageRepository>();
            services.AddScoped<IUserChatSessionRepository, UserChatSessionRepository>();
            services.AddScoped<IChatParticipantRepository,ChatParticipantRepository>();
            services.AddScoped<IUserDailyTaskRepository, UserDailyTaskRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();

        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IAuthenticationServices,AuthenticationServices>();
            services.AddScoped<IUserService,UserServices>();    
            services.AddScoped<IWellnessTaskService, WellnessTaskService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IReflectionServices, ReflectionServices>();
            services.AddScoped<IPsychologistServices,PsychologistServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IHabitServices,HabitServices>();
            services.AddScoped<INotificationsServices, NotificationsServices>();
            services.AddScoped<IPlanService,PlanService>();
            services.AddScoped<IUserPlanService,UserPlanService>(); 
            services.AddScoped<IHabitCategoryService, HabitCategoryService>();

        }

        public static void ConfigureHelper(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher,PasswordHasher>();
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            services.AddScoped<IJwtHelper,JwtHelper>();
            services.AddSignalR();
            services.AddHttpClient<IOpenRouterStreamService,OpenRouterStreamService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserFinder, UserFinder>();
            services.AddSingleton<IPushNotificationHelper,PushNotificationHelper>();
            services.AddScoped<IRazorViewToStringRenderer,RazorViewToStringRenderer>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<IOtpService, OtpService>();
            

        }

        public static void ConfigureMapProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}
