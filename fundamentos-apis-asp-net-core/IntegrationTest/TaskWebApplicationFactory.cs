using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TodoListApi.Controllers.Auth;
using TodoListApi.Data;
using static TodoListApi.Controllers.Auth.AuthController;

namespace IntegrationTest;


/**Essa classe nos possibilita criar uma instância da nossa API em execução na memória, que podemos reconfigurar e usar para testes.**/
public class TaskWebApplicationFactory: WebApplicationFactory<Program>
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            // Aqui você pode adicionar ou substituir serviços para testes, como bancos de dados em memória, mocks, etc.
            services.RemoveAll(typeof(DbContextOptions<TodoListDbContext>));


            // Pega a string de conexão com o banco
            var connectionString = context.Configuration.GetConnectionString("TestConnection");

            services.AddDbContext<TodoListDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // Cria um provedor para executar o Migrate
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TodoListDbContext>();
                // Aplica as migrations (cria as tabelas se ainda não existirem)
                db.Database.Migrate();
            }
        });
        base.ConfigureWebHost(builder);
    }

    public async Task<HttpClient> GetClientWithAcessTokenAsync()
    {
        var client = this.CreateClient();
        var user = new LoginDto { Email = "teste@teste.com", Password = "Teste@123" };
        var response = await client.PostAsJsonAsync("/api/auth/login", user);
       
        // Verifica se a resposta foi bem-sucedida
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AuthTokenResponse>();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result!.Token);

        return client;
    }

}
