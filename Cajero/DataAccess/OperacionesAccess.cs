using Cajero.Models.DAO;
using Cajero.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cajero.DataAccess
{
    public class OperacionesAccess : DBAccess<Operaciones>
    {
        public ContextDB _contextDB;

        public OperacionesAccess(ContextDB contextDB) : base(contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task<PaginatedList<HistorialOperacionesDTO>> Operaciones(long numeroTarjeta, int page = 1, int pageSize = 10)
        {
            var tarjeta = await _contextDB.Tarjetas.Where(x => x.NumeroTarjeta == numeroTarjeta).FirstOrDefaultAsync();
            var usuario = await _contextDB.Usuarios.Where(x => x.Id == tarjeta.IdUsuario).FirstOrDefaultAsync();
            var query = _contextDB.Operaciones.Where(x => x.IdUsuario == usuario.Id)
                .OrderByDescending(x => x.FechaOperacion);

            var totalCount = await query.CountAsync();

            var historialOperaciones = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var historialOperacionesDTO = new List<HistorialOperacionesDTO>();
            foreach (var item in historialOperaciones)
            {
                var tipoop = await _contextDB.TipoOperacion.Where(x => x.Id == item.TipoOperacion).FirstOrDefaultAsync();

                UsuarioHistorialDTO user = new()
                {
                    Nombre = item.Usuario.Nombre,
                    NumeroDeCuenta = item.Usuario.NumeroDeCuenta
                };

                TipoOperacionDto tipoOpDto = new();
                if (item.OperacionTipo != null)
                {
                    tipoOpDto.Nombre = tipoop.Nombre;
                }

                HistorialOperacionesDTO operacionDto = new()
                {
                    Monto = item.Monto,
                    IdUsuario = item.IdUsuario,
                    FechaOperacion = item.FechaOperacion,
                    Usuario = user,
                    TipoOperacion = item.TipoOperacion,
                    OperacionTipo = tipoOpDto
                };
                historialOperacionesDTO.Add(operacionDto);
            }

            return new PaginatedList<HistorialOperacionesDTO>(historialOperacionesDTO, totalCount, page, pageSize);
        }

    }
}
