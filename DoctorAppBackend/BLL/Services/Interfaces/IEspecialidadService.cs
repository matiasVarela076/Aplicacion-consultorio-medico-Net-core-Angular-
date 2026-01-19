using Models.DTO;

namespace BLL.Services.Interfaces
{
    public interface IEspecialidadService
    {
        Task<IEnumerable<EspecialidadDto>> GetAll();

        Task<EspecialidadDto> Add(EspecialidadDto modelDto);

        Task Update(EspecialidadDto modelDto);

        Task Remove(int id);
    }
}
