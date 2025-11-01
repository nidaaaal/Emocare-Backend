using AutoMapper;
using Emocare.Application.DTOs.Plan;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Payment;
using Emocare.Domain.Interfaces.Repositories.Payment;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Services
{
    public class PlanService:IPlanService
    {
        private readonly IPlanRepository _planRepo;
        private readonly IMapper _mapper;

        public PlanService(IPlanRepository planRepo,IMapper mapper)
        {
            _planRepo = planRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<Plan>>> GetPlan()
        {
           var plans =await _planRepo.GetAll();
            return ResponseBuilder.Success(plans, "All Plans Fetched", "PlanService");
        }
        public async Task<ApiResponse<GetPlan>?> GetPlanById(int id)
        {
            var res = await _planRepo.GetById(id) ?? throw new NotFoundException ("No plan Found on the corresponding id");

            var plan= _mapper.Map<GetPlan>(res);

            return ResponseBuilder.Success(plan, "Plan Found Plans Fetched", "PlanService");

        }
        public async Task<ApiResponse<string>> AddPlan(AddPlan dto)
        {
           if(await _planRepo.GetByName(dto.Name)) return ResponseBuilder.Fail<string>("Plan already Exist with same name", "PlanService",500);

           if (await _planRepo.GetByPrice(dto.Price)) return ResponseBuilder.Fail<string>("Plan already Exist with same price", "PlanService", 500);

           var plan = _mapper.Map<Plan>(dto);

            await _planRepo.Add(plan);

            return ResponseBuilder.Success("New Plan Added", "Plan Added to Plans", "PlanService");

        }
        public async Task<ApiResponse<string>?> UpdatePlan(int id, AddPlan dto)
        {
           var res = await _planRepo.GetById(id) ?? throw new NotFoundException("No plan Found on the corresponding id");

            var plan = _mapper.Map(dto, res);

            await _planRepo.Update(res);

            return ResponseBuilder.Success("New Plan Updated", "Plan Updated", "PlanService");

        }
        public async Task<ApiResponse<string>?> DeletePlan(int id)
        {
            var res = await _planRepo.GetById(id) ?? throw new NotFoundException("No plan Found on the corresponding id");
            res.IsDelete = true;
            await _planRepo.Update(res);
            return ResponseBuilder.Success("Plan Deleted", "Plan Updated", "PlanService");

        }
    }
}
