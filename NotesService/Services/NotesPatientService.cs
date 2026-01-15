using MongoDB.Driver;
using NotesService.Models;

namespace NotesService.Services
{
    public class NotesPatientService
    {
        // Récupère la collection de notes de patients depuis la base de données MongoDB
        public readonly IMongoCollection<PatientNote> _notes;

        public NotesPatientService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDb:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDb:DatabaseName"]);
            _notes = database.GetCollection<PatientNote>("notes");
        }

        public List<PatientNote> GetByPatientId(int patientId) =>
            _notes.Find(note => note.PatientId == patientId).ToList();
    }
}
