using RiskAssessmentService.Models.DTOs;

namespace RiskAssessmentService.Clients.Interfaces
{
    public interface IPatientsClient
    {
        Task<PatientDTO> GetPatientByIdAsync(string patientId, string bearerToken);
    }
}
