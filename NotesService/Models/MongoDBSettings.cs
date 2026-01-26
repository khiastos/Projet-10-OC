namespace NotesService.Models
{
    // Sert à récupérer les infos de connexion à MongoDB depuis appsettings.json
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
