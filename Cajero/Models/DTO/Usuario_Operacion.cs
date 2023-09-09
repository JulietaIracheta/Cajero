using Cajero.Models.DAO;

namespace Cajero.Models.DTO
{
    public class Usuario_Operacion
    {
        public string Nombre { get; set; }
        public long NumeroDeCuenta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime UltimaExtraccion { get; set; }
        public int MontoARetirar { get; set; }
        public DateTime FechaOperacion { get; set; }
        public List<OperacionDTO> Operaciones { get; set; }
    }
}
