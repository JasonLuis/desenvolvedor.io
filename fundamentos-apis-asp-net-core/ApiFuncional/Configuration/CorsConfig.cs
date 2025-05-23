namespace ApiFuncional.Configuration;

public static class CorsConfig
{
    public static WebApplicationBuilder AddCorsConfig(this WebApplicationBuilder builder)
    {
        /*Condifigura��o para dar suporte ao CORS*/
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Development", builder =>
                builder.AllowAnyOrigin() // permite qualquer origem
                       .AllowAnyMethod() // permite qualquer verbo
                       .AllowAnyHeader()); // permite qualquer header


            options.AddPolicy("Production", builder =>
                builder.WithOrigins("https://www.seusite.com") // permite apenas a origem especificada
                       .WithMethods("POST") // permite apenas o verbo especificado
                       .AllowAnyHeader()); // permite qualquer header

        });

        return builder;
    }
}
