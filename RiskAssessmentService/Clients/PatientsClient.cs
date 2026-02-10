using System.Net.Http.Headers;
using RiskAssessmentService.Clients.Interfaces;
using RiskAssessmentService.Models.DTOs;

namespace RiskAssessmentService.Clients
{
    public class PatientsClient : IPatientsClient
    {
        private readonly HttpClient _httpClient;

        public PatientsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PatientDTO> GetPatientByIdAsync(string patientId, string bearerToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", bearerToken.Replace("Bearer ", ""));

            var response = await _httpClient.GetAsync($"/api/patients/{patientId}");
            response.EnsureSuccessStatusCode();

            var patient = await response.Content.ReadFromJsonAsync<PatientDTO>();

            if (patient == null)
            {
                throw new InvalidOperationException("Patient not found");
            }

            return patient;
        }
    }
}
