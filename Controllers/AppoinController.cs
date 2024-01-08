using MedicApi.Data;
using MedicApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MedicApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AppoinController : ControllerBase
    {
      
        private readonly ApiContext _context;
        public AppoinController(ApiContext context)
        {
            _context = context;
        }
        [HttpPost]
        public JsonResult Create(Appoin ap)
        {
            /* _context.Appoins.Add(ap);
             _context.SaveChanges();
             return new JsonResult(Ok(ap));*/
            /* if (ap.PersonneId == 0)
             {
                 return new JsonResult(BadRequest("PatientId is required."));
             }

             // Vérifiez si le patient associé existe
             var patientExists = _context.Personnes.Any(p => p.Id == ap.PersonneId);
             if (!patientExists)
             {
                 return new JsonResult(BadRequest($"Patient with ID {ap.PersonneId} not found."));
             }

             // Ajoutez le rendez-vous à la base de données
             _context.Appoins.Add(ap);
             _context.SaveChanges();

             return new JsonResult(Ok(ap));*/
            if (ap.PersonneId == 0)
            {
                return new JsonResult(BadRequest("PersonneId is required."));
            }

            // Vérifiez si le patient associé existe
            var patientExists = _context.Personnes.Any(p => p.Id == ap.PersonneId);
            if (!patientExists)
            {
                return new JsonResult(BadRequest($"Personne with ID {ap.PersonneId} not found."));
            }

            // Ajoutez le rendez-vous à la base de données
            _context.Appoins.Add(ap);
            _context.SaveChanges();

            // Retournez uniquement les détails du rendez-vous
            var result = new
            {
                rid = ap.Rid,
                jour = ap.Jour,
                date = ap.Date,
                heure = ap.Heure,
                urgence = ap.Urgence,
                personneId = ap.PersonneId
            };

            return new JsonResult(Ok(result));
        }
        [HttpGet]
        public JsonResult GetAll() 
        {
            var result = _context.Appoins.ToList();
            return new JsonResult(Ok(result));
        }
        [HttpGet]
        public JsonResult Get(int id) 
        {
            var result = _context.Appoins.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(Ok(result));
        }
        [HttpPut]
        public JsonResult Edit(Appoin appoin)
        {
            var ap=_context.Appoins.Find(appoin.Rid);
            if (ap == null)
                return new JsonResult(NotFound());

            ap.Jour = appoin.Jour;
            ap.Date= appoin.Date;
            _context.SaveChanges();
            return new JsonResult(Ok(ap));
        }
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Appoins.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Appoins.Remove(result);
            _context.SaveChanges();
            return new JsonResult(Ok(result));
        }
        [HttpGet("{patientId}")]
        public IActionResult GetAppointmentsByPatient(int patientId)
        {
            /*   // Recherchez les rendez-vous associés à un patient spécifique
               var appointments = _context.Appoins.Where(a => a.PatientId == patientId).ToList();

               if (appointments == null || appointments.Count == 0)
               {
                   return new JsonResult(NotFound($"No appointments found for patient with ID {patientId}"));
               }

               return new JsonResult(Ok(appointments));*/
            var appointments = _context.Appoins
       .Join(_context.Personnes, a => a.PersonneId, p => p.Id, (a, p) => new { Appointment = a, Patient = p })
       .Where(ap => ap.Appointment.PersonneId == patientId)
       .Select(ap => new
       {
           AppointmentId = ap.Appointment.Rid,
           Jour = ap.Appointment.Jour,
           Rendre = ap.Appointment.Date,
           Patient = new
           {
               Id = ap.Patient.Id,
               Nom = ap.Patient.Nom,
               Prenom = ap.Patient.Prenom,
               Age = ap.Patient.Age,
               DateNaissance = ap.Patient.DateNaissance,
               Email = ap.Patient.Email,
               MotDePasse = ap.Patient.MotDePasse,
               Adresse = ap.Patient.Adresse,
               Role = ap.Patient.Role
           }
       })
            .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                return new JsonResult(NotFound($"No appointments found for patient with ID {patientId}"));
            }

            return new JsonResult(Ok(appointments));
        }
    }
}
