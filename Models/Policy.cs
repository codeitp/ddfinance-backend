using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyAPI.Models
{
    public class Policy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string PolicyName { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public decimal Premium { get; set; }

        [Range(1, int.MaxValue)]
        public decimal CoverageAmount { get; set; }

        [Required]
        public string PolicyHolderName { get; set; } = string.Empty;

        [Required]
        public DateTime EffectiveDate { get; set; }
    }



}
