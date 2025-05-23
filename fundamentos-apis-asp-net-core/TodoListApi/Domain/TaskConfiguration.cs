using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListApi.Models;

namespace TodoListApi.Domain
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnType("VARCHAR(255)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("VARCHAR(255)").IsRequired();
            builder.Property(x => x.Status).HasConversion<string>().IsRequired();
            builder.Property(x => x.DtCreated).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();


            // configura o relacionamento um para muitos 1:N
            builder.HasOne(t => t.User)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
