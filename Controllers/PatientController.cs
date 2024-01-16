 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicApi.Data;
using MedicApi.Models;
using System.Net.Mail;
using System.Net;

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
                    Role = 0 
                };

                _context.Personnes.Add(patient);
                _context.SaveChanges();
                SendWelcomeEmail(patient.Email, patient.Nom, patient.MotDePasse);
                return new JsonResult(Ok(patient));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest($"Erreur lors de l'ajout du patient : {ex.Message}"));
            }
        }
        private void SendWelcomeEmail(string toEmail, string patientName, string password)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("cabinettestdot@gmail.com", "vknj tcxt mibx wtgz"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("cabinettestdot@gmail.com"),
                Subject = "Bienvenue sur notre plateforme médicale",
                Body = $"Bonjour {patientName},\n\n" +
                       "Bienvenue sur notre plateforme médicale. Votre compte a été créé avec succès.\n" +
                       $"Votre mot de passe est : {password}\n\n" +
                       "Connectez-vous dès maintenant et profitez de nos services.\n\n" +
                       "Cordialement,\nVotre équipe médicale",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(toEmail);
            smtpClient.Send(mailMessage);
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
                    Role = 1 
                };

                _context.Personnes.Add(infirmier);
                _context.SaveChanges();
                return new JsonResult(Ok(infirmier));
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
