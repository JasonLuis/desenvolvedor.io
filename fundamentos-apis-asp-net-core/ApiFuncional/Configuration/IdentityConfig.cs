using ApiFuncional.Data;
using ApiFuncional.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiFuncional.Configuration;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        // Adiciona o Identity ao contêiner de serviços
        /* Informa ao AspNet que estou adicionando um midleware que vai configurar o sistema padrão de Identidade
         especificado na minha Identidade/usario e na minha role */

        // IdentityUser - representa o usuario logado
        // IdentityRole - representa o perfil do usuario logado
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<ApiDbContext>(); // contextto de criação das tabelas do Identity

        /**Configurando o JWT***/

        // Pegando o Token e gerando a chave encodada
        var JwtSettingsSection = builder.Configuration.GetSection("JwtSettings"); // pega a seção do appsettings
        builder.Services.Configure<JwtSettings>(JwtSettingsSection); // registra a seção no contêiner de serviços

        var jwtSettings = JwtSettingsSection.Get<JwtSettings>(); // pega as configurações do JWT
        var key = Encoding.ASCII.GetBytes(jwtSettings.Segredo); // pega a chave secreta do JWT e codifica em bytes

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // define o esquema de autenticação padrão
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // define o esquema de desafio padrão
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true; // requer HTTPS
            options.SaveToken = true; // salva o token
            options.TokenValidationParameters = new TokenValidationParameters // parâmetros de validação do token
            {
                IssuerSigningKey = new SymmetricSecurityKey(key), // chave simétrica de segurança
                ValidateIssuer = true, // valida o emissor
                ValidateAudience = true, // valida o público
                ValidIssuer = jwtSettings.Emissor, // emissor válido
                ValidAudience = jwtSettings.Audiencia, // público válido
            };
        });

        /**********************/

        return builder;
    }
}