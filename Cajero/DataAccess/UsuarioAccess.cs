using Cajero.Models.DAO;
using Cajero.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Cajero.DataAccess
{
    public class UsuarioAccess : DBAccess<Usuario>
    {
        public ContextDB _contextDB;

        public UsuarioAccess(ContextDB contextDB) : base(contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task<Usuario> Saldo(long numeroTarjeta)
        {
            var searchTarjeta = await _contextDB.Tarjetas.Where(x => x.NumeroTarjeta == numeroTarjeta).FirstOrDefaultAsync();
            var usuario = await _contextDB.Usuarios.Where(x => x.Id == searchTarjeta.IdUsuario).FirstOrDefaultAsync();

            TipoOperacion tipoOperacion = new()
            {
                Nombre = OperacionesConstantes.ConsultaSaldo
            };

            Operaciones operacion = new()
            {
                FechaOperacion = DateTime.Now,
                IdUsuario = usuario.Id,
                Monto = (int)usuario.Saldo,
                OperacionTipo = tipoOperacion
            };

            //_context.Operaciones.Update(operacion);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario_Operacion> Retiro(long numeroTarjeta, int monto)
        {
            var searchTarjeta = await _contextDB.Tarjetas.Where(x => x.NumeroTarjeta == numeroTarjeta).FirstOrDefaultAsync();
            var usuario = await _contextDB.Usuarios
                                .Include(x => x.Operaciones)
                                .Where(x => x.Id == searchTarjeta.IdUsuario).FirstOrDefaultAsync();

            TipoOperacion tipoOpe = new()
            {
                Nombre = OperacionesConstantes.Retiro
            };

            List<Operaciones> listOperaciones = new();

            var tipoOperacion = await _contextDB.TipoOperacion.Where(x => x.Nombre.Contains("Retiro")).FirstOrDefaultAsync();

            Operaciones operacion = new()
            {
                FechaOperacion = DateTime.Now,
                IdUsuario = usuario.Id,
                Monto = monto,
                //OperacionTipo = tipoOpe,
                TipoOperacion = tipoOperacion.Id

            };

            listOperaciones.Add(operacion);

            _context.Operaciones.Add(operacion);
            await _contextDB.SaveChangesAsync();

            List<OperacionDTO> listaOperacionDto = new();
            OperacionDTO operacionDTO = new()
            {
                FechaOperacion = DateTime.Now,
                TipoOperacion = OperacionesConstantes.Retiro
            };
            listaOperacionDto.Add(operacionDTO);
            if (usuario.Saldo >= monto)
            {
                usuario.Saldo -= monto;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
            }

            Usuario_Operacion usuario_Operacion = new()
            {
                FechaOperacion = DateTime.Now,
                MontoARetirar = monto,
                Nombre = usuario.Nombre,
                NumeroDeCuenta = usuario.NumeroDeCuenta,
                Saldo = usuario.Saldo,
                UltimaExtraccion = usuario.UltimaExtraccion,
                Operaciones = listaOperacionDto
            };

            return usuario_Operacion;
        }
    }
}
