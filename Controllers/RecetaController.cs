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
    public class RecetaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecetaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Receta
        [HttpGet]
        public async Task<ActionResult> GetReceta([FromQuery]FilterRecetaDTO? receta = null)
        {
            try
            {
                if (_context.Receta == null)
                {
                    return NotFound();
                }
                if (receta==null)
                {
                    return Ok(await _context.Receta.ToListAsync());
                }

                var listaRecetave = await _context.Receta.Include(x => x.Kategoria).Where
                    (
                    x => (!String.IsNullOrEmpty(receta.Emri)? x.Emri.Contains(receta.Emri) : true)&&
                    (!String.IsNullOrEmpty(receta.Udhezimet) ? x.Udhezimet.Contains(receta.Udhezimet) : true) &&
                    (receta.Kohezgjatja.HasValue ? x.Kohezgjatja==receta.Kohezgjatja : true) &&
                    (receta.Kalorite.HasValue ? x.Kalorite == receta.Kalorite : true) &&
                    (receta.Kohezgjatja.HasValue ? x.Kohezgjatja == receta.Kohezgjatja : true) &&
                    (!String.IsNullOrEmpty(receta.Kategoria) ? x.Kategoria.Emri.Contains(receta.Kategoria) : true)
                    ).ToListAsync();
                return Ok(listaRecetave);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // GET: api/Receta/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetReceta(int id)
        {
            try
            {
                if (_context.Receta == null)
                {
                    return NotFound();
                }
                var receta = await _context.Receta.FindAsync(id);

                if (receta == null)
                {
                    return NotFound();
                }

                return Ok(receta);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // PUT: api/Receta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceta(int id, EditRecetaDTO receta)
        {
            if (id != receta.Id)
            {
                return BadRequest();
            }
            var recete = new Receta
            {
                Id = id,
                Emri = receta.Emri,
                Kalorite = receta.Kalorite,
                KategoriaId = receta.KategoriaId,
                Kohezgjatja = receta.Kohezgjatja,
                Udhezimet=receta.Udhezimet
                
            };
            _context.Entry(recete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Edited successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetaExists(id))
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

        // POST: api/Receta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receta>> PostReceta(CreateRecetaDTO model)
        {
          if (_context.Receta == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Receta'  is null.");
          }
            var receta = new Receta
            {
                Emri = model.Emri,
                Udhezimet = model.Udhezimet,
                Kohezgjatja = model.Kohezgjatja,
                Kalorite = model.Kalorite,
                KategoriaId = model.KategoriaId
            };
            _context.Receta.Add(receta);
            try
            {
                await _context.SaveChangesAsync();
                return Ok("Added successfully!");
            }
            catch (DbUpdateException)
            {
                if (RecetaExists(receta.Id))
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

        // DELETE: api/Receta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            if (_context.Receta == null)
            {
                return NotFound();
            }
            var receta = await _context.Receta.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }

            try
            {
                _context.Receta.Remove(receta);
                await _context.SaveChangesAsync();

                return Ok("Deleted successfully!");
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        private bool RecetaExists(int id)
        {
            return (_context.Receta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
