using AutoMapper;
using Emocare.Application.DTOs.Chat;
using Emocare.Application.DTOs.Common;
using Emocare.Application.DTOs.Habits;
using Emocare.Application.DTOs.Plan;
using Emocare.Application.DTOs.Reflection;
using Emocare.Application.DTOs.Task;
using Emocare.Application.DTOs.User;
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Entities.Journal;
using Emocare.Domain.Entities.Payment;
using Emocare.Domain.Entities.Tasks;


namespace Emocare.Application.Mappings
{
    public class MapperProfile: Profile
    {
        public MapperProfile() 
        { 
            CreateMap<Users,UserRegisterDto>().ReverseMap();
            CreateMap<UserProfileDto,Users>().ReverseMap();    
            CreateMap<InsertTaskDto,WellnessTask>().ReverseMap();
            CreateMap<ChatMessage, ChatResponseDto>().ForMember(des => des.Role, opt => opt.MapFrom(x => x.Role.ToString()));
            CreateMap<JournalEntry, DailyResponseDto>().ForMember(des => des.Mood, opt => opt.MapFrom(x => x.Mood.ToString()))
                                                       .ForMember(des => des.Reflection, opt => opt.MapFrom(x => x.AIReflection));
            CreateMap<RequestSubscription,UserPushSubscription>();
            CreateMap<JournalEntry, AllQuotesDto>().ForMember(des => des.Reflection, opt => opt.MapFrom(x => x.AIReflection)).ReverseMap();

            CreateMap<Habit,AddHabit>().ReverseMap();

            CreateMap<GetPlan, Plan>().ReverseMap();
            CreateMap<AddPlan, Plan>();
            CreateMap<AddCategory, HabitCategory>();
            CreateMap<Habit,HabitDetails>();


            CreateMap<PsychologistProfile, GetPsychologistDto>()
    .ForMember(des => des.UserId, opt => opt.MapFrom(x => x.UserId))
    .ForMember(des => des.FullName, opt => opt.MapFrom(x => x.Users != null ? x.Users.FullName : string.Empty))
    .ForMember(des => des.Age, opt => opt.MapFrom(x => x.Users != null ? x.Users.Age : 0))
    .ForMember(des => des.EmailAddress, opt => opt.MapFrom(x => x.Users != null ? x.Users.EmailAddress : string.Empty))
    .ForMember(des => des.Gender, opt => opt.MapFrom(x => x.Users != null ? x.Users.Gender : string.Empty))
    .ForMember(des => des.Country, opt => opt.MapFrom(x => x.Users != null ? x.Users.Country : null))
    .ForMember(des => des.City, opt => opt.MapFrom(x => x.Users != null ? x.Users.City : null));

            CreateMap<InsertTaskDto, WellnessTask>()
    .ForMember(des => des.ImageUrl, opt => opt.Ignore()) 
    .ForMember(des => des.CreatedByPsychologistId, opt => opt.Ignore())
    .ForMember(des => des.PsychologistTaskAssignments, opt => opt.Ignore())
    .ForMember(des => des.UserDailyTasks, opt => opt.Ignore());





        }
    }
}
