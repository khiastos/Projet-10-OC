using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesService.Models;
using NotesService.Models.DTOs;
using NotesService.Services;

namespace NotesService.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    [Authorize(Roles = "Admin")]

    public class NotesController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        public NotesController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        // GET : api/notes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientNote>> GetNoteById(string id)
        {
            var note = await _mongoDBService.GetByIdAsync(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        // GET : api/notes/patient/{id}
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<PatientNote>>> GetNoteByPatient(int patientId)
        {
            var notes = await _mongoDBService.GetByPatientIdAsync(patientId);
            return Ok(notes);
        }

        // POST : api/notes
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDTO dto)
        {
            var note = new PatientNote
            {
                PatientId = dto.PatientId,
                Note = dto.Note
            };

            await _mongoDBService.CreateAsync(note);

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        // PUT : api/notes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(string id, [FromBody] PatientNote note)
        {
            await _mongoDBService.UpdateAsync(id, note);
            return NoContent();
        }

        // DELETE : api/notes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

    }
}
