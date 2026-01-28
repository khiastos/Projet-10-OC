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

        public async Task<PatientNote?> UpdateAsync(string id, string note)
        {
            // Crée un filtre pour trouver la note avec l'ID spécifié
            var filter = Builders<PatientNote>.Filter.Eq(n => n.Id, id);

            // Définit les mises à jour à appliquer à la note
            var update = Builders<PatientNote>.Update
                .Set(n => n.Note, note);

            // Applique la mise à jour à la note correspondante dans la collection
            await _notes.UpdateOneAsync(filter, update);

            return await _notes.Find(filter).FirstOrDefaultAsync();
        }
        public async Task DeleteAsync(string id)
        {
            await _notes.DeleteOneAsync(n => n.Id == id);
        }
    }
}
