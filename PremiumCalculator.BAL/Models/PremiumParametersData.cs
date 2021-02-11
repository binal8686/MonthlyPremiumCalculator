using System.ComponentModel.DataAnnotations;

namespace PremiumCalculator.BAL.Models
{
    public class PremiumParametersData
    {
        [Required]
        public int Age { get; set; }
        [Required]
        public int OccupationId { get; set; }
        [Required]
        public string SumInsured { get; set; }
    }

    public class PremiumParametersResponse
    {
        public decimal Premium { get; set; }
        public string Message { get; set; }
    }
}
