using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;
namespace MedicApi.Models
{
    public class Travail
    {
        public int Id { get; set; }
        public int TrHeure { get; set; }
        public string? Jours { get; set; }
        public bool Conge { get; set; }

        [ForeignKey(nameof(Personne))]
        public int PersonneId { get; set; } 
      
    }
}
