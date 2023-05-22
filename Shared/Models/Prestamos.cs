using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Prestamos
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public DateTime FechaPresta { get; set; }
        public DateTime? FechaDevop { get; set; }
        public int? UsuariosId { get; set; }
        public virtual Usuarios ? Usuarios { get; set; }
        public virtual ICollection<Libro>? Libros { get; set; } 
    }
}
