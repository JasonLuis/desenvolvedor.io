using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace TodoListApi.Models;

public class TaskItem
{

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset DtCreated { get; set; }
    public StatusTask Status { get; set; }

    // FK
    public Guid UserId { get; set; }

    // Navigation
    public User User { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusTask
{
    New,
    Progress,
    Complete,
}

// TODO:
// 1. Criar um container com o postregresql e configurar o ef core
// 2. Instalar o identity e configurar o relacionamento com a tabela Task
// 3. Criar as Migrations