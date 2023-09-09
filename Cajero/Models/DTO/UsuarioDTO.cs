using Cajero.Models.DAO;

namespace Cajero.Models.DTO
{
    public class UsuarioDTO
    {
        public string Nombre { get; set; }
        public long NumeroDeCuenta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime UltimaExtraccion { get; set; }
        public Operaciones Operaciones { get; set; }
        //public ICollection<TarjetaDTO> Tarjetas { get; set; }
    }
}
