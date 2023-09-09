using System.ComponentModel.DataAnnotations.Schema;

namespace Cajero.Models.DAO
{
    [Table("Operaciones")]
    public class Operaciones : Entidad
    {
        [Column("TipoOperacion")]
        [ForeignKey("TipoOperacion")]

        public int TipoOperacion { get; set; }

        [Column("Monto")]
        public int Monto { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("FechaOperacion")]
        public DateTime FechaOperacion { get; set; }
        public Usuario Usuario { get; set; }

        public TipoOperacion OperacionTipo { get; set; }

    }
}
