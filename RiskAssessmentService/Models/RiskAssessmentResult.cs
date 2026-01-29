namespace RiskAssessmentService.Models
{
    public class RiskAssessmentResult
    {
        public int Age { get; set; }
        public string Sex { get; set; }
        public int TriggerTermsCount { get; set; }
        public string RiskLevel { get; set; }
    }
}
