using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_6._0_API.Entities;
using ASP.NET_Core_6._0_API.DTO;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Core_6._0_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Kuzhinier")]
    public class PerberesiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PerberesiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Perberesi
        [HttpGet]
        public async Task<ActionResult> GetPerberesis()
        {
            try
            {
                if (_context.Perberesis == null)
                {
                    return NotFound();
                }
                return Ok(await _context.Perberesis.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // GET: api/Perberesi/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPerberesi(int id)
        {
            try
            {
                if (_context.Perberesis == null)
                {
                    return NotFound();
                }
                var perberesi = await _context.Perberesis.FindAsync(id);

                if (perberesi == null)
                {
                    return NotFound();
                }

                return Ok(perberesi);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // PUT: api/Perberesi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerberesi(int id, EditPerberesiDTO perberesi)
        {
            if (id != perberesi.Id)
            {
                return BadRequest();
            }
            var perberes = new Perberesi
            {
                Id = id,
                Emri = perberesi.Emri,
                RecetaId = perberesi.RecetaId
            };
            _context.Entry(perberes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Edited succesfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerberesiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Perberesi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPerberesi(CreatePerberesiDTO model)
        {
          if (_context.Perberesis == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Perberesis'  is null.");
          }
            var perberesi = new Perberesi
            {
                Emri = model.Emri,
                RecetaId = model.RecetaId
            };
            _context.Perberesis.Add(perberesi);
            try
            {
                await _context.SaveChangesAsync();
                return Ok("Added successfully!");
            }
            catch (DbUpdateException)
            {
                if (PerberesiExists(perberesi.Id))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest();
                    throw;
                }
            }
        }

        // DELETE: api/Perberesi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerberesi(int id)
        {
            if (_context.Perberesis == null)
            {
                return NotFound();
            }
            var perberesi = await _context.Perberesis.FindAsync(id);
            if (perberesi == null)
            {
                return NotFound();
            }

            try
            {
                _context.Perberesis.Remove(perberesi);
                await _context.SaveChangesAsync();
                return Ok("Deleted succesfully!");
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        private bool PerberesiExists(int id)
        {
            return (_context.Perberesis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
