namespace ApiFuncional.Configuration;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
                        .ConfigureApiBehaviorOptions(options => // Configurar as op��es de comportamento da API
                        {
                            options.SuppressModelStateInvalidFilter = true; // suprimir filtro invalido da model
                        });

        return builder;
    }
}