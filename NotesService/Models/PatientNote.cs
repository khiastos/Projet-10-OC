using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesService.Models
{
    public class PatientNote
    {
        // La primary key pour MongoDB
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Fait référence à l'ID du patient dans le service Patients
        [BsonElement("patientId")]
        public int PatientId { get; set; }

        // BsonElement = nom du champ dans la collection MongoDB
        [BsonElement("note")]
        public string Note { get; set; } = "";
    }
}
