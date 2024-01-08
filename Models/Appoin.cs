
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MedicApi.Models
{
    public class Appoin
    {
        public int Rid { get; set; }
        public string Jour { get; set; }
        public DateTime Date { get; set; }
        public string Heure { get; set; }
        public bool Urgence { get; set; }

        [ForeignKey(nameof(Personne))]
        public int PersonneId { get; set; } // Clé étrangère vers Personne
        
    }
}
