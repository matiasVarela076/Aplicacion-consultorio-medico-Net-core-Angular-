using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;

namespace Data.Servicio
{
    public class TokenService : ITokenServicio
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); //Lee la llave secreta del archivo de configuracion
        }

        public string CrearToken(Usuario usuario) //Crea un token JWT para el usuario autenticado
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName) //Agrega el nombre de usuario como un reclamo
            };

            SigningCredentials creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); //Crea las credenciales de firma usando la llave secreta y el algoritmo HMACSHA512

            SecurityTokenDescriptor tokeDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), //Establece los reclamos del token
                Expires = DateTime.Now.AddDays(7), //Establece la fecha de expiracion del token a 7 dias
                SigningCredentials = creds //Establece las credenciales de firma    
            };
            JwtSecurityTokenHandler tokehandler = new JwtSecurityTokenHandler(); //Crea un manejador de tokens JWT
            var token = tokehandler.CreateToken(tokeDescriptor); //Crea el token JWT usando el descriptor de token

            return tokehandler.WriteToken(token); //Devuelve el token JWT como una cadena
        }
    }
}
