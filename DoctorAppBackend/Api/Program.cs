using Api.Extensiones;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration); //Agregamos los servicios de la aplicacion
builder.Services.AddIdentityServices(builder.Configuration); //Agregamos los servicios de identidad

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<Api.Middleware.MiddlewareException>();
app.UseStatusCodePagesWithRedirects("/errores/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().
                   AllowAnyMethod().
                   AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
