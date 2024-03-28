using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_6._0_API.Entities;
using System.Collections;
using ASP.NET_Core_6._0_API.DTO;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Core_6._0_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class KategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Kategoria
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetKategoria()
        {
            try
            {
                if (_context.Kategoria == null)
                {
                    return NotFound();
                }
                return Ok( _context.Kategoria.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // GET: api/Kategoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetKategoria(int id)
        {
            try
            {
                if (_context.Kategoria == null)
                {
                    return NotFound();
                }
                var kategoria = await _context.Kategoria.FindAsync(id);

                if (kategoria == null)
                {
                    return NotFound();
                }

                return Ok(kategoria);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // PUT: api/Kategoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKategoria(int id, EditKategoriaDTO kategoria)
        {
            if (id != kategoria.Id)
            {
                return BadRequest();
            }
            var kategori = new Kategoria
            {
                Id = id,
                Emri = kategoria.Emri

            };
            _context.Entry(kategori).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Updated successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                    throw;
                }
            }
        }

        // POST: api/Kategoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostKategoria(CreateKategoriaDTO model)
        {
          if (_context.Kategoria == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Kategoria'  is null.");
          }
            var kategoria = new Kategoria
            {
                Emri = model.Emri
            };
            _context.Kategoria.Add(kategoria);
            try
            {
                await _context.SaveChangesAsync();
                return Ok("Added successfully!");
            }
            catch (DbUpdateException)
            {
                if (KategoriaExists(kategoria.Id))
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

        // DELETE: api/Kategoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKategoria(int id)
        {
            if (_context.Kategoria == null)
            {
                return NotFound();
            }
            var kategoria = await _context.Kategoria.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }

            _context.Kategoria.Remove(kategoria);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully");
        }

        private bool KategoriaExists(int id)
        {
            return (_context.Kategoria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
