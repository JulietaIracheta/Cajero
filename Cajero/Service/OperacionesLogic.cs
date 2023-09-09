using Cajero.DataAccess;
using Cajero.Models.DAO;
using Cajero.Models.DTO;

namespace Cajero.Service
{
    public class OperacionesLogic
    {
        private readonly OperacionesAccess _operacionesAccess;
        private readonly IConfiguration _configuration;

        public OperacionesLogic(OperacionesAccess operacionesAccess, IConfiguration configuration)
        {
            _operacionesAccess = operacionesAccess;
            _configuration = configuration;
        }

        public async Task<PaginatedList<HistorialOperacionesDTO>> Operaciones(long numeroTarjeta, int page, int pageSize)
        {
            var result = await _operacionesAccess.Operaciones(numeroTarjeta, page, pageSize);
            return result;
        }
    }
}
