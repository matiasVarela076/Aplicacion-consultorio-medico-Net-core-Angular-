using Models.Entidades;

namespace Data.Interfaces.IRepositorio
{
    public interface IEspecialidadRepositorio : IRepositorioGenerico<Especialidad>
    {
        void Update(Especialidad especialidad);
    }
}
