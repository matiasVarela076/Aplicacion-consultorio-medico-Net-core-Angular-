using System.Linq.Expressions;

namespace Data.Interfaces.IRepositorio
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
            string incluirPropiedades = null
            );

        Task<T> GetFirst(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null);

        Task Add(T entidad);

        void Remover(T entidad);
    }
}
