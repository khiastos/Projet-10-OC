namespace RiskAssessmentService.Models
{
    public class PatientRiskData
    {
        public string PatientId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Sex { get; set; }
    }
}
