using Emocare.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.DTOs.Plan
{
    #region Response
    public class GetPlan
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Duration { get; set; }
    }

    public class GetUserPlan
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public int PlanId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }

    #endregion


    #region Request
    public class AddPlan
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Duration { get; set; }
    }

    public class AddUserPlan
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int PlanId { get; set; }

    }



    #endregion



}
