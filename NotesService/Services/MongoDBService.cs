using MongoDB.Driver;
using NotesService.Models;

namespace NotesService.Services
{
    public class MongoDBService
    {
        // Récupère la collection de notes de patients depuis la base de données MongoDB
        public readonly IMongoCollection<PatientNote> _notes;

        // Récupère les infos de la BDD
        public MongoDBService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config["MongoDb:ConnectionURI"]);
            IMongoDatabase database = client.GetDatabase(config["MongoDb:DatabaseName"]);
            _notes = database.GetCollection<PatientNote>("notes");
        }
        public async Task<PatientNote?> GetByIdAsync(string id)
        {
            return await _notes
                .Find(n => n.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PatientNote>> GetByPatientIdAsync(int patientId)
        {
            return await _notes
                // Permet de filtrer les notes en fonction de l'ID du patient
                .Find(n => n.PatientId == patientId)
                .ToListAsync();
        }

        public async Task CreateAsync(PatientNote note)
        {
            await _notes.InsertOneAsync(note);
        }

        public async Task UpdateAsync(string id, PatientNote note)
        {
            await _notes.ReplaceOneAsync(n => n.Id == id, note);
        }
        public async Task DeleteAsync(string id)
        {
            await _notes.DeleteOneAsync(n => n.Id == id);
        }
    }
}
