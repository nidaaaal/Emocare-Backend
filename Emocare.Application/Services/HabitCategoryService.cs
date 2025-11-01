using AutoMapper;
using Emocare.Application.DTOs.Habits;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Shared.Helpers.Api;


namespace Emocare.Application.Services
{
    public class HabitCategoryService: IHabitCategoryService
    {
        private readonly IHabitCategoryRepository _habitCategoryRepository;
        private readonly IMapper _mapper;
        public HabitCategoryService(IHabitCategoryRepository habitCategoryRepository, IMapper mapper)
        {
            _habitCategoryRepository = habitCategoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<HabitCategory>>> GetAll()
        {
           var data = await _habitCategoryRepository.GetAll();
            return ResponseBuilder.Success(data, "All Category fetched", "HabitCategoryService");
        }
        public async Task<ApiResponse<string>> AddCategory(AddCategory category)
        {
           var data =  _mapper.Map<HabitCategory>(category);
            await _habitCategoryRepository.Add(data);
            return ResponseBuilder.Success("category added", "new Category added", "HabitCategoryService");

        }
    }
}
