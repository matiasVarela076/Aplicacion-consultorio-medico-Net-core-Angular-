using Data.Interfaces.IRepositorio;

namespace Data.Repositorio
{
    public class UnitForWork : IUnitOfWork
    {
       private readonly ApplicationDbContext _db;
      public IEspecialidadRepositorio EspecialidadRepositorio { get; private set; }

        public UnitForWork(ApplicationDbContext db)
        {
            _db = db;
            EspecialidadRepositorio = new EspecialidadRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveChangesAsync()
        {
           await _db.SaveChangesAsync();
        }
    }
}
