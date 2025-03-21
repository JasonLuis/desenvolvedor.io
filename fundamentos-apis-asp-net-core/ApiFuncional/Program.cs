using ApiFuncional.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options => // Configurar as op��es de comportamento da API
                {
                    options.SuppressModelStateInvalidFilter = true; // suprimir filtro invalido da model
                });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configura��o para dar suporte ao DbContext
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") // busca a string de conex�o no appsetting
     );
}); // registra o servico no contain

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
