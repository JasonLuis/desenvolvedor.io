using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations;


// Exemplo de como criar arquivos de configuração de modelo de dados de forma totalmente desaclopada
// Facilitando a manutenção

// curiosidade a interface IEntityTypeConfiguration, não estava disponível na primeira versão do EF Core, sendo implementada posteriormente

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(p => p.Telefone).HasColumnType("CHAR(11)");
        builder.Property(p => p.Cep).HasColumnType("CHAR(8)").IsRequired();
        builder.Property(p => p.Estado).HasColumnType("CHAR(2)").IsRequired();
        builder.Property(p => p.Cidade).HasMaxLength(60).IsRequired();
        // exemplo de como criar um indice, nesse exeplo foi criado um indice para telefone
        builder.HasIndex(i => i.Telefone).HasDatabaseName("idx_cliente_telefone");
    }
}
