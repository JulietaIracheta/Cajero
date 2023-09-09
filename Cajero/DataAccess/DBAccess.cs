using Cajero.Models.DAO;
using System.Data.Entity;

namespace Cajero.DataAccess
{
    public abstract class DBAccess<T> where T : EntidadDB
    {
        protected ContextDB _context;

        public DBAccess(ContextDB context)
        {
            _context = context;

            try
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                _context.ChangeTracker.LazyLoadingEnabled = false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();

        public async Task<T> Find(int id) => await _context.Set<T>().FindAsync(id);

        public async Task<List<T>> Get() => await _context.Set<T>().ToListAsync();

        public IQueryable<T> GetQuery() => _context.Set<T>();

        public void EnableAutoDetectChanges() => _context.ChangeTracker.AutoDetectChangesEnabled = true;

        public void DisableAutoDetectChanges() => _context.ChangeTracker.AutoDetectChangesEnabled = false;
    }
}
