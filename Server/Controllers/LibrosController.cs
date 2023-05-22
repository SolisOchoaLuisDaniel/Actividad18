using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad18.Server.Contexto;
using Actividad18.Shared.Models;

namespace Actividad18.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ContextoBibliotecaUDC _context;

        public LibrosController(ContextoBibliotecaUDC context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            var libros = await _context.Libro.Include(l => l.Prestamos).ToListAsync();
            return libros;
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibros(int id)
        {
            var libros = await _context.Libro.Include(l => l.Prestamos).FirstOrDefaultAsync(l => l.Id == id);

            if (libros == null)
            {
                return NotFound();
            }

            return libros;
        }

        // PUT: api/Libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibros(int id, Libro libros)
        {
            if (id != libros.Id)
            {
                return BadRequest();
            }

            _context.Entry(libros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id))
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

        // POST: api/Libros
        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibros(Libro libros)
        {
            if (libros.PrestamosId == 0 || libros.PrestamosId == null)
            {
                libros.PrestamosId = null;
            }

            _context.Libro.Add(libros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibros", new { id = libros.Id }, libros);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibros(int id)
        {
            var libros = await _context.Libro.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }

            _context.Libro.Remove(libros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _context.Libro.Any(e => e.Id == id);
        }

        // GET: api/Libros/Prestamos/5
        [HttpGet("Prestamos/{prestamosId}")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosByPrestamosId(int prestamosId)
        {
            var libros = await _context.Libro.Include(l => l.Prestamos).Where(l => l.PrestamosId == prestamosId).ToListAsync();

            if (libros == null || libros.Count == 0)
            {
                return NotFound();
            }

            return libros;
        }
    }
}
