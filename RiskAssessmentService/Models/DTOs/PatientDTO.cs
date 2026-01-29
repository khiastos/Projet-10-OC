namespace RiskAssessmentService.Models.DTOs
{
    public class PatientDTO
    {
        public string PatientId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
