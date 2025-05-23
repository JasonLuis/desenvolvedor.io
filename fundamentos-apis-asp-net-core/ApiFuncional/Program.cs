using System.Text;
using ApiFuncional.Configuration;
using ApiFuncional.Data;
using ApiFuncional.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.AddApiConfig()
       .AddCorsConfig()
       .AddSwaggerConfig()
       .AddDbContextConfig()
       .AddIdentityConfig();
        


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Development"); // usa a política de CORS de desenvolvimento
}
else
{
    app.UseCors("Production"); // usa a política de CORS de produção
}

app.UseHttpsRedirection();

// Nunca inverter app.UseAuthentication() com app.UseAuthorization()
// O middleware de autenticação deve ser chamado antes do middleware de autorização

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
