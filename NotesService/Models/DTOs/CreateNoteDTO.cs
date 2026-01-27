namespace NotesService.Models.DTOs
{
    public class CreateNoteDTO
    {
        public int PatientId { get; set; }
        public string Note { get; set; } = "";
    }
}
