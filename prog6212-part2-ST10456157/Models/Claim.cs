using System.ComponentModel.DataAnnotations;

namespace prog6212_part2_ST10456157.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required]
        public decimal HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public string? Notes { get; set; }
        public string? DocumentName { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
