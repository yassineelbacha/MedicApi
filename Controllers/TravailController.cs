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

        // GET: api/Travail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Travail>>> GetTravail()
        {
            // Vérifier si l'utilisateur a le rôle approprié (1 pour infirmier)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("1"))
            {
                if (_context.Travails == null)
                {
                    return NotFound();
                }
                return await _context.Travails.ToListAsync();
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        // GET: api/Travail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Travail>> GetTravail(int id)
        {
            // Vérifier si l'utilisateur a le rôle approprié (1 pour infirmier)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("1"))
            {
                if (_context.Travails == null)
                {
                    return NotFound();
                }
                var travail = await _context.Travails.FindAsync(id);

                if (travail == null)
                {
                    return NotFound();
                }

                return travail;
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        // PUT: api/Travail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravail(int id, Travail travail)
        {
            // Vérifier si l'utilisateur a le rôle approprié (1 pour infirmier)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("1"))
            {
                if (id != travail.Id)
                {
                    return BadRequest();
                }

                _context.Entry(travail).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravailExists(id))
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

        // POST: api/Travail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Travail>> PostTravail(Travail travail)
        {
            // Vérifier si l'utilisateur a le rôle approprié (1 pour infirmier)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("1"))
            {
                if (_context.Travails == null)
                {
                    return Problem("Entity set 'ApiContext.Travail' is null.");
                }
                _context.Travails.Add(travail);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTravail", new { id = travail.Id }, travail);
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        // DELETE: api/Travail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravail(int id)
        {
            // Vérifier si l'utilisateur a le rôle approprié (1 pour infirmier)
            if (User != null && User.Identity.IsAuthenticated && User.IsInRole("1"))
            {
                if (_context.Travails == null)
                {
                    return NotFound();
                }
                var travail = await _context.Travails.FindAsync(id);
                if (travail == null)
                {
                    return NotFound();
                }

                _context.Travails.Remove(travail);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return Unauthorized(); // L'utilisateur n'est pas autorisé
            }
        }

        private bool TravailExists(int id)
        {
            return (_context.Travails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
