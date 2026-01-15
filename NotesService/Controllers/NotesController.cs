using Microsoft.AspNetCore.Mvc;
using NotesService.Models;
using NotesService.Services;

namespace NotesService.Controllers
{
    [ApiController]
    [Route("notes")]
    public class NotesController : ControllerBase
    {
        private readonly NotesPatientService _notesService;

        public NotesController(NotesPatientService notesService)
        {
            _notesService = notesService;
        }

        [HttpGet("patient/{patientId}")]
        public ActionResult<List<PatientNote>> GetNotesByPatientId(int patientId)
        {
            var notes = _notesService.GetByPatientId(patientId);
            return Ok(notes);
        }
    }
}
