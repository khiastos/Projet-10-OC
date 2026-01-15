using System.ComponentModel.DataAnnotations;

namespace PatientsService.Models.DTOs
{
    public class UpdatePatientDTO
    {
        [Required, StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; } = "";

        [Required, StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; } = "";

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; } = null!;

        public string? Address { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
    }
}
