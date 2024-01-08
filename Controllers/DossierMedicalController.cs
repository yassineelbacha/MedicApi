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
    public class DossierMedicalController : ControllerBase
    {
        private readonly ApiContext _context;

        public DossierMedicalController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/DossierMedical
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DossierMedical>>> GetDossierMedical()
        {
            // Vérifier si l'utilisateur a le rôle approprié (0 pour patient)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("0"))
            {
                if (_context.DossierMedicals == null)
                {
                    return NotFound();
                }
                return await _context.DossierMedicals.ToListAsync();
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        // GET: api/DossierMedical/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DossierMedical>> GetDossierMedical(int id)
        {
            // Vérifier si l'utilisateur a le rôle approprié (0 pour patient)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("0"))
            {
                if (_context.DossierMedicals == null)
                {
                    return NotFound();
                }
                var dossierMedical = await _context.DossierMedicals.FindAsync(id);

                if (dossierMedical == null)
                {
                    return NotFound();
                }

                return dossierMedical;
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        // PUT: api/DossierMedical/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDossierMedical(int id, DossierMedical dossierMedical)
        {
            // Vérifier si l'utilisateur a le rôle approprié (0 pour patient)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("0"))
            {
                if (id != dossierMedical.Id)
                {
                    return BadRequest();
                }

                _context.Entry(dossierMedical).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DossierMedicalExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        // POST: api/DossierMedical
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DossierMedical>> PostDossierMedical(DossierMedical dossierMedical)
        {
            // Vérifier si l'utilisateur a le rôle approprié (0 pour patient)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("0"))
            {
                if (_context.DossierMedicals == null)
                {
                    return Problem("Entity set 'ApiContext.DossierMedical' is null.");
                }
                _context.DossierMedicals.Add(dossierMedical);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDossierMedical", new { id = dossierMedical.Id }, dossierMedical);
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        // DELETE: api/DossierMedical/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDossierMedical(int id)
        {
            // Vérifier si l'utilisateur a le rôle approprié (0 pour patient)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("0"))
            {
                if (_context.DossierMedicals == null)
                {
                    return NotFound();
                }
                var dossierMedical = await _context.DossierMedicals.FindAsync(id);
                if (dossierMedical == null)
                {
                    return NotFound();
                }

                _context.DossierMedicals.Remove(dossierMedical);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        private bool DossierMedicalExists(int id)
        {
            return (_context.DossierMedicals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
