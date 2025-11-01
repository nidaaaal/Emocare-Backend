
using Emocare.Application.DTOs.Task;
using Emocare.Domain.Entities.Tasks;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface IWellnessTaskService
    {
        Task<ApiResponse<IEnumerable<WellnessTask>>> GetTasksAsync();
        Task<ApiResponse<WellnessTask>?> GetTasksByIdAsync(int id);
        Task<ApiResponse<string>> AddTaskAsync(InsertTaskDto dto);
        Task<ApiResponse<string>?> UpdateTaskAsync(int id,InsertTaskDto dto);
        Task<ApiResponse<string>?> DeleteTaskAsync(int id);
        Task<ApiResponse<WellnessTask>> GetTodays();
        Task<ApiResponse<IEnumerable<WellnessTask>>> GetPsychologistTaskByIdAsync();
        Task<ApiResponse<IEnumerable<WellnessTask>>> GetPsychologistTaskAsync();
        Task<ApiResponse<IEnumerable<WellnessTask>>> GetAdminTasksAsync();





    }
}
