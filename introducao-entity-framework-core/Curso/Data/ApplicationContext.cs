using CursoEFCore.Data.Configurations;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace CursoEFCore.Data;

public class ApplicationContext : DbContext
{

    // Para instalar o pacote logger -> dotnet add package Microsoft.Extensions.Logging.Console --version 8.0.1
    private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

    // Posso informar para o EF Core qual entidade quero criar meu modelo de dados expondo minha enidade em uma propriedade DbSet em meu contexto
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    // através do OnConfiguring que informaremos qual o provider estaremos utilizando
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*
            site que mostra a string de conexão -> https://www.connectionstrings.com/sql-server/
        */

        optionsBuilder
            .UseLoggerFactory(_logger) // para utilizar o logger
            .EnableSensitiveDataLogging() // Quando estamos utilizando logging de aplicações, por padrão, todas as informações são sensiveis. Por padrão o EF core não exibe os valores que são gerados por ele, então para que possamos ver o valor por parametro gerado por ele é necessario habilitar essa opção através desse metodo.
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CursoEFCore;Integrated Security=True", p=>p.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null).MigrationsHistoryTable("curso_ef_core")); // Habilita a opção de retentativa de conexão, caso a conexão falhe, ele tenta novamente 3 vezes com um intervalo de 5 segundos entre as tentativas e também podemos adicionar uma lista de erros que queremos que ele tente novamente.

            // MigrationsHistoryTable("curso_ef_core") -> Altera o nome da tabela de historico de migrações para o nome que eu quiser, nesse caso para curso_ef_core
    }

    // Posso informar para o EF Core qual entidade quero criar meu modelo de dados informando no OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        // Mapeando atraves do Fluent API
        // exemplo usando o OnModelCreating
        // modelBuilder.Entity<PedidoItem>( p => 
        // {
        //     p.ToTable("PedidoItens");
        //     p.HasKey(p => p.Id);
        //     p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
        //     p.Property(p => p.Valor).IsRequired();
        //     p.Property(p => p.Desconto).IsRequired();
        // });

        // para inserir minhas classes de configuração de modelo de dados no OnModelCreating.

        // 1 - posso fazer dessa maneira, inserindo as classes uma por vez
        // modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        
        // 2 - Dessa maneira estou dizendo para procurar todas as classes que estou usando a interface IEntityTypeConfiguration nesse assembly que estou executando, diminuindo a quantidade de linha da forma anterior.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        // chama a função para mapear as propriedades esquecidas
        MapearPropriedadesEsquecidas(modelBuilder);
    }

    private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // pega as propriedades que são do tipo string
            var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

            foreach (var property in properties)
            {
                // Verifica se o tipo da coluna esta vazia e se não tem um tamanho para propriedade
                if (string.IsNullOrEmpty(property.GetColumnType()) && 
                !property.GetMaxLength().HasValue)
                {
                    // property.SetMaxLength(100);
                    property.SetColumnType("VARCHAR(100)");
                }
            }
        }
    }
}
