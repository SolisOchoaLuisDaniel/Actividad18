using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Libro
    {
        public int? Id { get; set; }
        public string? titulo { get; set; }
        public string? autor { get; set; }
        public DateTime? Fecha { get; set; }
        public string? ISBN { get; set; }
        public int? disponible { get; set; }

        public int? PrestamosId { get; set; }
        public virtual Prestamos? Prestamos { get; set; }
    }
}
