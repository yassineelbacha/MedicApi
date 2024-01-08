using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedicApi.Data;
namespace MedicApi.Models
{
    public class DossierMedical
    {
        public int Id { get; set; }
        public string? Descriptions { get; set; }
        public string? Conclusion { get; set; }
        public string? Medicaments { get; set; }
        public string? Certificats { get; set; }

        [ForeignKey(nameof(Personne))]
        public int PersonneId { get; set; } // Clé étrangère vers Personne
      
    }


}
