using Microsoft.AspNetCore.Mvc;
using NotesService.Models;
using NotesService.Models.DTOs;
using NotesService.Services;

namespace NotesService.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    //[Authorize(Roles = "Admin")]

    public class NotesController : ControllerBase
    {
        private readonly NoteService _noteService;
        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        // GET : api/notes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientNote>> GetNoteById(string id)
        {
            var note = await _noteService.GetByIdAsync(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        // GET : api/notes/patient/{id}
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<PatientNote>>> GetNoteByPatient(int patientId)
        {
            var notes = await _noteService.GetByPatientIdAsync(patientId);
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

            await _noteService.CreateAsync(note);

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        // PUT : api/notes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(string id, [FromBody] UpdateNoteDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Note))
                return BadRequest("Une note ne peut pas être vide");

            var updatedNote = await _noteService.UpdateAsync(id, dto.Note);

            if (updatedNote == null)
                return NotFound();

            return Ok(updatedNote);
        }

        // DELETE : api/notes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            await _noteService.DeleteAsync(id);
            return NoContent();
        }

    }
}
