using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Controlador base para no andar repitiendo codigo 
    public class BaseApiController : ControllerBase
    {
    }
}
