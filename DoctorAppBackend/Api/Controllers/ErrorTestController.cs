using Api.Errores;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entidades;

namespace Api.Controllers
{
    public class ErrorTestController : BaseApiController
    {
        private readonly ApplicationDbContext _db;

        public ErrorTestController(ApplicationDbContext db)
        {
           _db = db;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetNotAuthorizew()
        {
            return "No autorizado";
        }

        [HttpGet("not-found")]
        public ActionResult<Usuario> GetNotFound()
        {
            var obj = _db.Usuarios.Find(-1);

            if (obj == null)
            {
                return NotFound(new ApiErrorResponse(404));
            }
            else
            {
                return obj;
            }
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var obj = _db.Usuarios.Find(-1);
            var objString = obj.ToString();

            return objString;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));
        }
    }
}
