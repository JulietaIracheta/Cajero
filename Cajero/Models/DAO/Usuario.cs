using Cajero.Models.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cajero.Models.DAO
{
    [Table("Usuarios")]
    public class Usuario : Entidad
    {
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("NumeroDeCuenta")]
        public long NumeroDeCuenta { get; set; }

        [Column("Saldo")]
        public decimal Saldo { get; set; }

        [Column("UltimaExtraccion")]
        public DateTime UltimaExtraccion { get; set; }

        public ICollection<Tarjeta> Tarjetas { get; set; }
        public ICollection<Operaciones> Operaciones { get; set; }

    }
}
