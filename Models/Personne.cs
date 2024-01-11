
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace MedicApi.Models
{
    public class Personne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public string Adresse { get; set; }
        
        public int Role { get; set; }
    }
}
