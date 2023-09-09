using Cajero.DataAccess;
using Cajero.Models.DAO;
using Cajero.Models.DTO;

namespace Cajero.Service
{
    public class UsuarioLogic
    {
        private readonly UsuarioAccess _usuarioAccess;
        private readonly IConfiguration _configuration;

        public UsuarioLogic(UsuarioAccess usuarioAccess, IConfiguration configuration)
        {
            _usuarioAccess = usuarioAccess;
            _configuration = configuration;
        }

        public async Task<UsuarioDTO> Saldo(long numeroTarjeta)
        {
            var result = await _usuarioAccess.Saldo(numeroTarjeta);

            UsuarioDTO usuario = new UsuarioDTO()
            {
                Nombre = result.Nombre,
                NumeroDeCuenta = result.NumeroDeCuenta,
                Saldo = result.Saldo,
                UltimaExtraccion = result.UltimaExtraccion
            };

            return usuario;
        }

        public async Task<Usuario_Operacion> Retiro(long numeroTarjeta, int monto)
        {
            var result = await _usuarioAccess.Retiro(numeroTarjeta, monto);

            //UsuarioDTO usuario = new()
            //{
            //    Nombre = result.Nombre,
            //    NumeroDeCuenta = result.NumeroDeCuenta,
            //    Saldo = result.Saldo,
            //    UltimaExtraccion = result.UltimaExtraccion
            //    //Operaciones = result.Operacion
            //};

            return result;
        }
    }
}
