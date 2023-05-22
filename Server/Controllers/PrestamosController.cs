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
    public class PrestamosController : ControllerBase
    {
        private readonly ContextoBibliotecaUDC _context;

        public PrestamosController(ContextoBibliotecaUDC context)
        {
            _context = context;
        }

        // GET: api/Prestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamos>>> GetPrestamos()
        {
            if (_context.Prestamos == null)
            {
                return NotFound();
            }

            // Incluir la información de los usuarios vinculados y los libros vinculados
            return await _context.Prestamos.Include(p => p.Usuarios).Include(p => p.Libros).ToListAsync();
        }

        // GET: api/Prestamos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamos>> GetPrestamos(int id)
        {
            if (_context.Prestamos == null)
            {
                return NotFound();
            }
            var prestamos = await _context.Prestamos.Include(p => p.Usuarios).Include(p => p.Libros
            ).FirstOrDefaultAsync(p => p.Id == id);

            if (prestamos == null)
            {
                return NotFound();
            }

            return prestamos;
        }

        // PUT: api/Prestamos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamos(int id, Prestamos prestamos)
        {
            if (id != prestamos.Id)
            {
                return BadRequest();
            }

            _context.Entry(prestamos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestamosExists(id))
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

        // POST: api/Prestamos
        [HttpPost]
        public async Task<ActionResult<Prestamos>> PostPrestamos(Prestamos prestamos)
        {
            if (_context.Prestamos == null)
            {
                return Problem("Entity set 'ContextoBibliotecaUDC.Prestamos' is null.");
            }

            // Obtener el usuario correspondiente al ID proporcionado
            var usuario = await _context.Usuarios.FindAsync(prestamos.UsuariosId);
            if (usuario == null)
            {
                return BadRequest("El usuario especificado no existe.");
            }

            prestamos.Usuarios = usuario;

            _context.Prestamos.Add(prestamos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrestamos", new { id = prestamos.Id }, prestamos);
        }

        // DELETE: api/Prestamos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamos(int id)
        {
            if (_context.Prestamos == null)
            {
                return NotFound();
            }
            var prestamos = await _context.Prestamos.FindAsync(id);
            if (prestamos == null)
            {
                return NotFound();
            }

            _context.Prestamos.Remove(prestamos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrestamosExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
