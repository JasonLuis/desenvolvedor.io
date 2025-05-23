using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;

namespace TodoListApi.Data;

public class TodoListDbContext : IdentityDbContext<User, Roles, Guid>
{
    public DbSet<TaskItem> Tasks { get; set; }

    public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Chamada necessária para configurar as entidades do Identity
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoListDbContext).Assembly);
    }
}
