using System.ComponentModel.DataAnnotations;

namespace Tarea_1.Models
{
    public class EntidadArticulo
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public System.Decimal Precio { get; set; }
    }
}
