using System;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.IniciandoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(p => p.Status).HasConversion<string>();
        builder.Property(p => p.TipoFrete).HasConversion<int>();
        builder.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

        // Configura o relacionamento muitos para um
        builder.HasMany(p => p.Items)
            .WithOne(p => p.Pedido)
            // DeleteBehavior.Cascade -> Dessa forma, quanto excluimos um pedido, excluimos os items desse pedido sera deletado automaticamento
            // DeleteBehavior.Restrict -> Primeiramente tenho que excluir os items para excluir o pedido
            .OnDelete(DeleteBehavior.Cascade);
    }
}
