using RiskAssessmentService.Models.DTOs;

namespace RiskAssessmentService.Clients.Interfaces
{
    public interface INotesClient
    {
        Task<IEnumerable<NoteDTO>> GetNotesByPatientIdAsync(string patientId);
    }
}
