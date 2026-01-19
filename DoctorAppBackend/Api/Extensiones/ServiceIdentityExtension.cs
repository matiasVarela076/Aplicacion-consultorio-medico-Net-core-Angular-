using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensiones
{
    public static class ServiceIdentityExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Agrega la autenticacion usando JWT Bearer
                             .AddJwtBearer(opcions =>
                             {
                                 opcions.TokenValidationParameters = new TokenValidationParameters
                                 {
                                     ValidateIssuerSigningKey = true,
                                     IssuerSigningKey = new SymmetricSecurityKey(
                                               Encoding.UTF8.GetBytes(config["TokenKey"])),
                                     ValidateIssuer = false,
                                     ValidateAudience = false
                                 };
                             });
            return services;
        }
    }
}
