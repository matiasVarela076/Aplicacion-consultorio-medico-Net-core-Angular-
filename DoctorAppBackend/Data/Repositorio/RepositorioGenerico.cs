using System.Linq.Expressions;
using Data.Interfaces.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositorio
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public RepositorioGenerico(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task Add(T entidad)
        {
            await _dbSet.AddAsync(entidad);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null, string incluirPropiedades = null)
        {
          IQueryable<T> query = _dbSet;
            
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirPropiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirPropiedad);
                }
            }

            if (orderBy != null)
            {
                return await query.ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = _dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirPropiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirPropiedad);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public void Remover(T entidad)
        {
            _dbSet.Remove(entidad);
        }
    }
}
