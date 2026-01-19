using BLL.Services;
using BLL.Services.Interfaces;
using Data;
using Data.Interfaces;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;
using Data.Servicio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Utilidades;

namespace Api.Extensiones
{
    public static class ServiceAppExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opcion =>
            {
                opcion.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingresar Bearer [espacio] [token] \r\n\r\n " +
                                  "Ejemplo: Bearer ejoyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                opcion.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
            });
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(opcions => opcions.UseSqlServer(connectionString)); //usamos el conection string para conectarnos a la base de datos sql server
            services.AddCors();
            services.AddScoped<ITokenServicio, TokenService>();
            services.Configure<ApiBehaviorOptions>(opcion =>
            {
                opcion.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errores = actionContext.ModelState
                                  .Where(e => e.Value.Errors.Count > 0)
                                  .SelectMany(x => x.Value.Errors)
                                  .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new Errores.ApiValidacionErrorResponse
                    {
                        errores = errores
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddScoped<IUnitOfWork, UnitForWork>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IEspecialidadService, EspecialidadService>();

            return services;
        }
    }
}
