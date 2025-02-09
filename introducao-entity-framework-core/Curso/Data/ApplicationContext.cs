using CursoEFCore.Data.Configurations;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data;

public class ApplicationContext : DbContext
{

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

        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CursoEFCore;Integrated Security=True");
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
    }
}
