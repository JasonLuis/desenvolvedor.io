using System.Text;
using ApiFuncional.Data;
using ApiFuncional.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options => // Configurar as op��es de comportamento da API
                {
                    options.SuppressModelStateInvalidFilter = true; // suprimir filtro invalido da model
                });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // Adiciona a definição de segurançaa Bearer
    {
        Description = "Insira o token JWT desta maneira: Bearer {seu token}",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement // Adiciona a exigência de segurança
    {
        { new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { } }
    });
});

// configura��o para dar suporte ao DbContext
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") // busca a string de conex�o no appsetting
     );
}); // registra o servico no contain

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

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Nuca inverter app.UseAuthentication() com app.UseAuthorization()
// O middleware de autenticação deve ser chamado antes do middleware de autorização

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
