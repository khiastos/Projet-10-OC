namespace PatientsService.Models.DTOs
{
    public class CreatePatientDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
    }
}
