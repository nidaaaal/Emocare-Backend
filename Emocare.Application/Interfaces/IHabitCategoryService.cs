using Emocare.Application.DTOs.Habits;
using Emocare.Domain.Entities.Habits;
using Emocare.Shared.Helpers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.Interfaces
{
    public interface IHabitCategoryService
    {
        Task<ApiResponse<IEnumerable<HabitCategory>>> GetAll();  
        Task<ApiResponse<string>> AddCategory(AddCategory category);

    }
}
