using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cajero.Models.DAO
{
    public abstract class Entidad : EntidadDB
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}
