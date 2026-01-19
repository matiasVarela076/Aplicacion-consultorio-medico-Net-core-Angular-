using System.Net;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Api.Controllers
{
    public class EspecialidadController : BaseApiController
    {
        private readonly IEspecialidadService _especialidadService;

        private ApiResponse _response;

        public EspecialidadController(IEspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _especialidadService.GetAll();
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EspecialidadDto modelDto)
        {
            try
            {
                await _especialidadService.Add(modelDto);;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(EspecialidadDto modelDto)
        {
            try
            {
                await _especialidadService.Update(modelDto);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _especialidadService.Remove(id);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                
                _response.Message = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
