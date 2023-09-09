using System.ComponentModel.DataAnnotations.Schema;

namespace Cajero.Models.DAO
{
    [Table("Tarjetas")]
    public class Tarjeta : Entidad
    {
        [Column("NumeroTarjeta")]
        public long NumeroTarjeta { get; set; }

        [Column("Pin")]
        public int Pin { get; set; }

        [Column("Bloqueada")]
        public bool Bloqueada { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("IntentosFallidos")]
        public int IntentosFallidos { get; set; }
        public Usuario Usuario { get; set; }
    }
}
