namespace NotesService.Models.DTOs
{
    public class UpdateNoteDTO
    {
        public int PatientId { get; set; }
        public string Note { get; set; } = "";

    }
}
