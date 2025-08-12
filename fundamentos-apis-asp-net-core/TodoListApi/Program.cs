using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TodoListApi.Data;
using TodoListApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // converte string para ENUM
                }); ;

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{

   options.SwaggerDoc("v1", new() { Title = "TaskList API", Version = "v1" });

    /* Para configurar minha query string como ENUM */
    options.UseInlineDefinitionsForEnums(); 
    options.MapType<StatusTask>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(StatusTask)).Select(name => new OpenApiString(name)).ToList<IOpenApiAny>()
    });
    /***/

    // Configura o esquema de seguran�a
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Informe o token JWT como: Bearer {seu token}",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// substitui o AddSwagger que era usado antes nas vers�es anteriores
// builder.Services.AddOpenApi(); // por padr�o � v1.json 

builder.Services.AddDbContext<TodoListDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/* 
 * User - representa o usu�rio logado, herda de IdentityUser
 * Roles - representa o perfil do usu�rio, herda de IdentityRole 
 */
builder.Services.AddIdentity<User, Roles>()
    .AddRoles<Roles>()
    .AddEntityFrameworkStores<TodoListDbContext>();

// JWT

// Pegando o token da variavel de conex�o e gerando a chave encodada
var JwtSettingsSection = builder.Configuration.GetSection("JwtSettings"); // pega os dados do appsettings
builder.Services.Configure<JwtSettings>(JwtSettingsSection); // registra a se��o do cont�iner de servi�os

var jwtSettings = JwtSettingsSection.Get<JwtSettings>(); // pega as configura��es do JWT
var key = Encoding.ASCII.GetBytes(jwtSettings!.Secret!); //  pega a chave secreta do JWT e codifica em bytes

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // define o esquema de autentica��o padr�o
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // define o esquema de desafio padr�o
})
.AddGoogle(opt =>
{
    opt.ClientId = builder.Configuration["Authentication:Google:ClientId"]; // pega o ClientId do Google
    opt.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]; // pega o ClientSecret do Google
    opt.CallbackPath = "/signin-google";
})
.AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = true; // requer HTTPS
    opt.SaveToken = true; // salva o token
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key), // chave sim�trica de seguran�a
        ValidateIssuer = true, // valida o emissor
        ValidateAudience = true, // valida o p�blico
        ValidIssuer = jwtSettings.Issuer, // emissor v�lido
        ValidAudience = jwtSettings.Audience, // p�blico v�lido
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // app.MapScalarApiReference(); // mapeia um endpoint de documenta��o da API em /scalar. � equivalente ao Swagger (em /swagger)
    // Nesse projeto, utilizaremos o swagger para visualizar a documenta��o da API
}

app.UseDeveloperExceptionPage();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }