using Actividad18.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Actividad18.Server.Contexto
{
    public class ContextoBibliotecaUDC : DbContext
    {
        public ContextoBibliotecaUDC(DbContextOptions<ContextoBibliotecaUDC> options) : base (options) 
        {
            
        }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
    }
}
