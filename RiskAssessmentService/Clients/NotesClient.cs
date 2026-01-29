using RiskAssessmentService.Clients.Interfaces;
using RiskAssessmentService.Models.DTOs;

namespace RiskAssessmentService.Clients
{
    public class NotesClient : INotesClient
    {
        private readonly HttpClient _httpClient;

        public NotesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<NoteDTO>> GetNotesByPatientIdAsync(string patientId)
        {
            var response = await _httpClient.GetAsync($"/api/notes/patient/{patientId}");

            response.EnsureSuccessStatusCode();

            var notes = await response.Content.ReadFromJsonAsync<IEnumerable<NoteDTO>>();

            return notes ?? Enumerable.Empty<NoteDTO>();
        }
    }
}
