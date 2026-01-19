using System.Security.Cryptography;
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Entidades;

namespace Api.Controllers
{

    public class UsuarioController : BaseApiController 
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenServicio _tokenServicio;

        public UsuarioController(ApplicationDbContext db, ITokenServicio tokenServicio)
        {
            _db = db;
            _tokenServicio = tokenServicio;
        }

        [Authorize]
        [HttpGet] //api/usuario
        public async Task< ActionResult <IEnumerable<Usuario>>> GetUsusarios()
        {
            var Usuarios = await _db.Usuarios.ToListAsync();
            return Ok(Usuarios);
        }

        [Authorize]
        [HttpGet("{id}")] //api/usuario/1]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// Genera un hash y una salt para la contraseña usando HMACSHA512,
        /// guarda el usuario con `PaswordHash` y `PaswordSalt` y persiste los cambios.
        /// </summary>
        /// <param name="registroDto">DTO con `UserName` y `Password` para el registro.</param>
        /// <returns>
        /// Devuelve el usuario creado envuelto en <see cref="ActionResult{Usuario}"/>.
        /// </returns>
        [HttpPost("registro")] //POST: api/usuario/registro
        public async Task<ActionResult<UsuarioDto>> Registro (RegistroDto registroDto)
        {                       
            if(await UsuarioExiste(registroDto.UserName)) return BadRequest("el usuario ya esta registrado");

            using HMACSHA512 hmac = new HMACSHA512(); //Genera la llave y el hash, se usa el using para liberar memoria

            Usuario usuario = new Usuario
            {
                UserName = registroDto.UserName.ToLower(),
                PaswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registroDto.Password)),
                PaswordSalt = hmac.Key
            };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            
            return new UsuarioDto
            {
                UserName = usuario.UserName,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        [HttpPost("login")] // POST: api/usuario/login
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            Usuario usuario = await _db.Usuarios.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            
            if (usuario == null) return Unauthorized("Usuario no valido");
            
            using HMACSHA512 hmac = new HMACSHA512(usuario.PaswordSalt);

            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != usuario.PaswordHash[i]) return Unauthorized("password no valido");
            }

            return new UsuarioDto
            {
                UserName = usuario.UserName,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        private async Task<bool> UsuarioExiste(string userName)
        {
            return await _db.Usuarios.AnyAsync(x => x.UserName == userName.ToLower());
        }
    }
}
