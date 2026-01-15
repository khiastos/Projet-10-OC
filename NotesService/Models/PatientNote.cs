using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesService.Models
{
    public class PatientNote
    {
        // La primary key pour MongoDB
        [BsonId]
        public ObjectId Id { get; set; }

        // fait le lien entre le nom ici et celui donné dans MongoDB
        [BsonElement("patientId")]
        public int PatientId { get; set; }

        [BsonElement("note")]
        public string Note { get; set; } = "";
    }
}
