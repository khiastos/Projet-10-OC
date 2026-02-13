using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiskAssessmentService.Clients.Interfaces;
using RiskAssessmentService.Models;
using RiskAssessmentService.Models.DTOs;
using RiskAssessmentService.Services.Interfaces;

namespace RiskAssessmentService.Controllers
{
    [ApiController]
    [Route("api/risk-assessment")]
    [Authorize(Roles = "Admin")]
    public class RiskAssessmentController : ControllerBase
    {
        private readonly IPatientsClient _patientsClient;
        private readonly INotesClient _notesClient;
        private readonly IRiskAssessmentService _riskAssessmentService;

        public RiskAssessmentController(IPatientsClient patientsClient, INotesClient notesClient, IRiskAssessmentService riskAssessmentService)
        {
            _patientsClient = patientsClient;
            _notesClient = notesClient;
            _riskAssessmentService = riskAssessmentService;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRiskAssessment(string patientId)
        {

            var token = HttpContext.Request.Headers["Authorization"].ToString();

            var patient = await _patientsClient.GetPatientByIdAsync(patientId, token);
            var notes = await _notesClient.GetNotesByPatientIdAsync(patientId, token);

            // Appel aux microservices pour obtenir les données nécessaires
            PatientDTO patientDTO = await _patientsClient.GetPatientByIdAsync(patientId, token);
            IEnumerable<NoteDTO> notesDTO = await _notesClient.GetNotesByPatientIdAsync(patientId, token);

            // Mapping des patients aux modèles internes
            var patientRiskData = new PatientRiskData
            {
                PatientId = patientDTO.PatientId,
                DateOfBirth = patientDTO.DateOfBirth,
                Sex = patientDTO.Gender
            };

            // Mapping des notes
            var notesData = notesDTO.Select(note => new NoteData
            {
                Note = note.Note
            });

            // Calcul du risque
            var result = await _riskAssessmentService.RiskAssessmentCalcul(patientRiskData, notesData);

            return Ok(result);
        }
    }
}
