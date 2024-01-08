 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicApi.Data;
using MedicApi.Models;

namespace MedicApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PersonneController : ControllerBase
    {
        private readonly ApiContext _context;
        public PersonneController(ApiContext context)
        {
            _context = context;
        }
        [HttpPost]
        public JsonResult Create(Personne p)
        {
            _context.Personnes.Add(p);
            _context.SaveChanges();
            return new JsonResult(Ok(p));
        }
        [HttpPost]
        public JsonResult AddPatient(Personne patientDto)
        {
            try
            {
                var patient = new Personne
                {
                    Nom = patientDto.Nom,
                    Prenom = patientDto.Prenom,
                    Age = patientDto.Age,
                    DateNaissance = patientDto.DateNaissance,
                    Email = patientDto.Email,
                    MotDePasse = patientDto.MotDePasse,
                    Adresse = patientDto.Adresse,
                    Role = 0 // 0 pour patient
                };

                _context.Personnes.Add(patient);
                _context.SaveChanges();
                return new JsonResult(Ok("Patient ajouté avec succès"));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest($"Erreur lors de l'ajout du patient : {ex.Message}"));
            }
        }

        [HttpPost]
        public JsonResult AddInfirmier(Personne infirmierDto)
        {
            try
            {
                var infirmier = new Personne
                {
                    Nom = infirmierDto.Nom,
                    Prenom = infirmierDto.Prenom,
                    Age = infirmierDto.Age,
                    DateNaissance = infirmierDto.DateNaissance,
                    Email = infirmierDto.Email,
                    MotDePasse = infirmierDto.MotDePasse,
                    Adresse = infirmierDto.Adresse,
                    Role = 1 // 1 pour infirmier
                };

                _context.Personnes.Add(infirmier);
                _context.SaveChanges();
                return new JsonResult(Ok("Infirmier ajouté avec succès"));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest($"Erreur lors de l'ajout de l'infirmier : {ex.Message}"));
            }
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Personnes.ToList();
            return new JsonResult(Ok(result));
        }
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Personnes.Find(id);
            if(result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }
        [HttpGet]
        public JsonResult GetPatients()
        {
            var patients = _context.Personnes.Where(p => p.Role == 0).ToList();
            return new JsonResult(Ok(patients));
        }

        [HttpGet]
        public JsonResult GetInfirmiers()
        {
            var infirmiers = _context.Personnes.Where(p => p.Role == 1).ToList();
            return new JsonResult(Ok(infirmiers));
        }
        [HttpGet]
        public JsonResult Getdocteur()
        {
            var docteur = _context.Personnes.Where(p => p.Role == 2).ToList();
            return new JsonResult(Ok(docteur));
        }

        [HttpGet]
        public JsonResult Authenticate([FromQuery] string email, [FromQuery] string motDePasse)
        {
            var user = _context.Personnes
                .SingleOrDefault(u => u.Email == email && u.MotDePasse == motDePasse);

            if (user == null)
            {
                return new JsonResult(new { Message = "Informations d'authentification incorrectes" }) { StatusCode = 401 };
            }

            // Supprimez ou masquez les informations sensibles avant de les renvoyer au client
            user.MotDePasse = null;

            return new JsonResult(new { Message = "Authentification réussie", User = user }) { StatusCode = 200 };
        }

        [HttpPut]
        public JsonResult Edit(Personne p) 
        {
            var pat = _context.Personnes.Find(p.Id);
            if(pat == null)
                return new JsonResult(NotFound());

            pat.Nom = p.Nom;
            pat.Prenom = p.Prenom;
            pat.Age = p.Age;
            pat.DateNaissance = p.DateNaissance;
            pat.Email = p.Email;
            pat.MotDePasse = p.MotDePasse;
            pat.Adresse = p.Adresse;
            _context.SaveChanges();
            return new JsonResult(Ok(p));
        }
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Personnes.Find(id);
            if (result == null)
                return new JsonResult(NotFound());

            _context.Personnes.Remove(result);
            _context.SaveChanges();
            return new JsonResult(Ok(result));
        }
    }
}
