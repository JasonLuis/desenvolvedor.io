using ApiFuncional.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiFuncional.Configuration;

public static class DbContextConfig
{
    public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
    {
        // configura��o para dar suporte ao DbContext
        builder.Services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection") // busca a string de conex�o no appsetting
             );
        }); // registra o servico no contain

        return builder;
    }
}
