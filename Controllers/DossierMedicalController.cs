using MedicApi.Data;
using MedicApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MedicApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DossierMedicalController : ControllerBase
    {
        private readonly ApiContext _context;

        public DossierMedicalController(ApiContext context)
        {
            _context = context;
        }

        private int? GetRoleByPersonneId(int personneId)
        {
            var personne = _context.Personnes.FirstOrDefault(p => p.Id == personneId);
            return personne?.Role;
        }

        [HttpPost]
        public JsonResult Create(DossierMedical d)
        {
            if (d.PersonneId == 0)
            {
                return new JsonResult(BadRequest("PersonneId is required."));
            }

            var personneRole = GetRoleByPersonneId(d.PersonneId);

            // Vérifier si le rôle de la personne est approprié (0 pour patient)
            if (personneRole.HasValue && personneRole.Value == 0)
            {
                var patientExists = _context.Personnes.Any(p => p.Id == d.PersonneId);
                if (!patientExists)
                {
                    return new JsonResult(BadRequest($"Personne with ID {d.PersonneId} not found."));
                }

                _context.DossierMedicals.Add(d);
                _context.SaveChanges();

                var result = new
                {
                    Id = d.Id,
                    description = d.Descriptions,
                    conclusion = d.Conclusion,
                    medicament = d.Medicaments,
                    certificat = d.Certificats,
                    personneId = d.PersonneId
                };

                return new JsonResult(Ok(result));
            }

            // Si le rôle n'est pas approprié, retourner une réponse BadRequest
            return new JsonResult(BadRequest("Unauthorized: Invalid role."));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            // Ajoutez ici la vérification du rôle si nécessaire
            var result = _context.DossierMedicals.ToList();
            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            // Ajoutez ici la vérification du rôle si nécessaire
            var result = _context.DossierMedicals.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            // Ajoutez ici la vérification du rôle si nécessaire
            var result = _context.DossierMedicals.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.DossierMedicals.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));
        }
        [HttpPut]
     
        public JsonResult Edit(DossierMedical updatedDossier)
        {
            var dossier = _context.DossierMedicals.Find(updatedDossier.Id);

            if (dossier == null)
            {
                return new JsonResult(NotFound());
            }

            // Mettre à jour les informations du dossier médical
            dossier.Descriptions = updatedDossier.Descriptions;
            dossier.Conclusion = updatedDossier.Conclusion;
            dossier.Medicaments = updatedDossier.Medicaments;
            dossier.Certificats = updatedDossier.Certificats;

            _context.SaveChanges();

            
            return new JsonResult(Ok(updatedDossier));
        }
    }
}
