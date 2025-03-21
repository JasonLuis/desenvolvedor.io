var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // mecanismo para ajudar a mapear os endpoints
builder.Services.AddSwaggerGen(); // exposi��o da documenta��o da API

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // se o ambiente for desenvolvimento, exp�e o Swagger
{
    app.UseSwagger(); // registra o middleware
    app.UseSwaggerUI(); // registra o middleware de interface
}

app.UseHttpsRedirection(); // redirecionamento de HTTPS

app.UseAuthorization(); // validar autentica��o dos usuarios 

app.MapControllers(); // mapear todas as controllers. Cria um dicionrio de rotas

// Uma outra forma de mapear rotas
app.MapGet("/", () => "Acesse /swagger para mais informa��es"); 
app.MapGet("/teste-get", () => "Teste");

app.Run(); // rodar a aplica��o