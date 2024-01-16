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

            return new JsonResult(BadRequest("Unauthorized: Invalid role."));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            
            var result = _context.DossierMedicals.ToList();
            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            
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

            dossier.Descriptions = updatedDossier.Descriptions;
            dossier.Conclusion = updatedDossier.Conclusion;
            dossier.Medicaments = updatedDossier.Medicaments;
            dossier.Certificats = updatedDossier.Certificats;

            _context.SaveChanges();

            
            return new JsonResult(Ok(updatedDossier));
        }
        [HttpGet("{patientId}")]
        public IActionResult GetDocumentByPatient(int patientId)
        {
            var dossiers = _context.DossierMedicals
       .Join(_context.Personnes, d => d.PersonneId, p => p.Id, (d, p) => new { DossierMedical = d, Patient = p })
       .Where(dm => dm.DossierMedical.PersonneId == patientId)
       .Select(dm => new
       {

           Dossierid = dm.DossierMedical.Id,
           Description = dm.DossierMedical.Descriptions,
           Conclusion = dm.DossierMedical.Conclusion,
           Medicament = dm.DossierMedical.Medicaments,
           Certificat = dm.DossierMedical.Certificats,
           Patient = new
           {
               Id = dm.Patient.Id,
               Nom = dm.Patient.Nom,
               Prenom = dm.Patient.Prenom,
               Age = dm.Patient.Age,
               DateNaissance = dm.Patient.DateNaissance,
               Email = dm.Patient.Email,
               MotDePasse = dm.Patient.MotDePasse,
               Adresse = dm.Patient.Adresse,
               Role = dm.Patient.Role
           }
       })
            .ToList();

            if (dossiers == null || dossiers.Count == 0)
            {
                return new JsonResult(NotFound($"No Dossier found for patient with ID {patientId}"));
            }

            return new JsonResult(Ok(dossiers));
        }
    }
}
