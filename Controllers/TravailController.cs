using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicApi.Data;
using MedicApi.Models;

namespace MedicApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TravailController : ControllerBase
    {
        private readonly ApiContext _context;

        public TravailController(ApiContext context)
        {
            _context = context;
        }

        private int? GetRoleByPersonneId(int personneId)
        {
            var personne = _context.Personnes.FirstOrDefault(p => p.Id == personneId);
            return personne?.Role;
        }
        [HttpPost]
        public JsonResult Create(Travail t)
        {
            if(t.PersonneId == 0)
            {
                return new JsonResult(BadRequest("PersonneId is required."));
            }
            var personneRole = GetRoleByPersonneId(t.PersonneId);
            if (personneRole.HasValue && personneRole.Value == 1)
            {
                var patientExists = _context.Personnes.Any(p => p.Id == t.PersonneId);
                if (!patientExists)
                {
                    return new JsonResult(BadRequest($"Personne with ID {t.PersonneId} not found."));
                }
                _context.Travails.Add(t);
                _context.SaveChanges();
                var result = new
                {
                    id = t.Id,
                    hrheure =  t.TrHeure,
                    jours = t.Jours,
                    conge = t.Conge,
                    personneid = t.PersonneId
                };
                return new JsonResult(Ok(result));
            }
            return new JsonResult(BadRequest("Unauthorized: Invalid role."));
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Travails.ToList();
            return new JsonResult(Ok(result));
        }
        [HttpGet]
        public JsonResult Get(int id)
        {
            // Ajoutez ici la vérification du rôle si nécessaire
            var result = _context.Travails.Find(id);
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
            var result = _context.Travails.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Travails.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));
        }
        [HttpPut]

        public JsonResult Edit(Travail updatedTravail)
        {
            var tr = _context.Travails.Find(updatedTravail.Id);

            if (tr == null)
            {
                return new JsonResult(NotFound());
            }

            // Mettre à jour les informations du dossier médical
            tr.TrHeure = updatedTravail.TrHeure;
            tr.Jours = updatedTravail.Jours;
            tr.Conge = updatedTravail.Conge;

            _context.SaveChanges();


            return new JsonResult(Ok(updatedTravail));
        }
    }
}
