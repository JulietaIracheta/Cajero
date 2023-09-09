using Cajero.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace Cajero.DataAccess
{
    public class CajeroAccess : DBAccess<Tarjeta>
    {
        public ContextDB _contextDB;

        public CajeroAccess(ContextDB contextDB) : base(contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task<bool> ValidarDatos(long numeroTarjeta, int pin)
        {
            var result = await _contextDB.Tarjetas.Where(x => x.NumeroTarjeta == numeroTarjeta && x.Pin == pin).FirstOrDefaultAsync();

            if (result == null)
            {
                var searchTarjeta = await _contextDB.Tarjetas.Where(x => x.NumeroTarjeta == numeroTarjeta).FirstOrDefaultAsync();

                if (searchTarjeta != null)
                {
                    if (searchTarjeta.Bloqueada == false)
                    {

                        if (searchTarjeta.Pin == pin)
                        {
                            searchTarjeta.IntentosFallidos = 0;
                            await _contextDB.SaveChangesAsync();
                            return true;
                        }

                        else
                        {
                            searchTarjeta.IntentosFallidos++;

                            if (searchTarjeta.IntentosFallidos == 4)
                            {
                                searchTarjeta.Bloqueada = true;
                            }

                            _contextDB.Tarjetas.Update(searchTarjeta);
                            await _contextDB.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<Tarjeta> SearchTarjeta(long numeroTarjeta)
        {
            var result = await _contextDB.Tarjetas.Where(x => x.NumeroTarjeta == numeroTarjeta).FirstOrDefaultAsync();

            return result;
        }
    }
}
