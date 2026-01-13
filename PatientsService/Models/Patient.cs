using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Back.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le prénom du patient est obligatoire")]
        [DisplayName("Prénom")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Le nom du patient est obligatoire")]
        [DisplayName("Nom")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "La date de naissance du patient est obligatoire")]
        [DisplayName("Date de naissance")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Le genre du patient est obligatoire")]
        [DisplayName("Genre")]
        public string Gender { get; set; } = "";

        [DisplayName("Adresse")]
        public string? Address { get; set; }

        [DisplayName("Numéro de téléphone")]
        public string? PhoneNumber { get; set; }
    }
}
