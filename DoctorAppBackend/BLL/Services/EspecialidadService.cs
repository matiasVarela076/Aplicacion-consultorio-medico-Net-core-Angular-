using System.Data;
using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTO;
using Models.Entidades;

namespace BLL.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IMapper _mapper;

        public EspecialidadService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EspecialidadDto> Add(EspecialidadDto modelDto)
        {
            try
            {
                Especialidad especialidad = new Especialidad
                {
                    NombreEspecialidad = modelDto.NombreEspecialidad,
                    Descripcion = modelDto.Descripcion,
                    Estado = modelDto.Estado == 1 ? true : false,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };
                await _unitOfWork.EspecialidadRepositorio.Add(especialidad);

                await _unitOfWork.SaveChangesAsync();

                if (especialidad.Id < 0)
                {
                    throw new TaskCanceledException("La especialidad no se puedo crear "); 
                }
                return _mapper.Map<EspecialidadDto>(especialidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task Update(EspecialidadDto modelDto)
        {
            try
            {
                var especialidadDb = await _unitOfWork.EspecialidadRepositorio.GetFirst(e => e.Id == modelDto.Id);

                if (especialidadDb == null)
                {
                    throw new TaskCanceledException("La especialdad no existe");
                }
                especialidadDb.NombreEspecialidad = modelDto.NombreEspecialidad;
                especialidadDb.Descripcion = modelDto.Descripcion;
                especialidadDb.Estado = modelDto.Estado == 1 ? true : false;

                _unitOfWork.EspecialidadRepositorio.Update(especialidadDb);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task Remove(int id)
        {
            try
            {
                var especialidadDb = await _unitOfWork.EspecialidadRepositorio.GetFirst(e => e.Id == id);

                if (especialidadDb == null)
                {
                    throw new TaskCanceledException("La especialdad no existe");
                }
                _unitOfWork.EspecialidadRepositorio.Remover(especialidadDb);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EspecialidadDto>> GetAll()
        {
            try
            {
               var lista = await _unitOfWork.EspecialidadRepositorio.GetAll(
                   orderBy : e => (IOrderedEnumerable<Especialidad>)e.OrderBy(e => e.NombreEspecialidad));

                return _mapper.Map<IEnumerable<EspecialidadDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<IEnumerable<EspecialidadDto>> GetAll()
        //{
        //    try
        //    {
        //        var list = await _unitOfWork.EspecialidadRepositorio.GetAll(
        //                            orderBy: q => (IOrderedEnumerable<Especialidad>)q.OrderBy(e => e.NombreEspecialidad));//esto quedo raro
        //        return _mapper.Map<IEnumerable<EspecialidadDto>>(list);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
    }


}
