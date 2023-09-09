using Cajero.Models.DAO;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cajero.Models.DTO
{
    public class HistorialOperacionesDTO
    {
        public int TipoOperacion { get; set; }
        public int Monto { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaOperacion { get; set; }
        public UsuarioHistorialDTO Usuario { get; set; }
        public TipoOperacionDto OperacionTipo { get; set; }
    }
}
