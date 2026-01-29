using RiskAssessmentService.Models;

namespace RiskAssessmentService.Services.Interfaces
{
    public interface IRiskAssessmentService
    {
        Task<RiskAssessmentResult> RiskAssessmentCalcul(PatientRiskData patientRiskData, IEnumerable<NoteData> notesData);
    }
}
