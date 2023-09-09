using System.ComponentModel.DataAnnotations.Schema;

namespace Cajero.Models.DAO
{
    [Table("TipoOperacion")]
    public class TipoOperacion : Entidad
    {
        [Column("Nombre")]
        public string Nombre { get; set; }
        public ICollection<Operaciones> Operaciones { get; set; }

    }
}
