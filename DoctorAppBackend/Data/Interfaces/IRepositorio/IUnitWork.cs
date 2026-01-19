namespace Data.Interfaces.IRepositorio
{
    public interface IUnitOfWork : IDisposable
    {
        IEspecialidadRepositorio EspecialidadRepositorio { get; }
        Task SaveChangesAsync();
    }
}
