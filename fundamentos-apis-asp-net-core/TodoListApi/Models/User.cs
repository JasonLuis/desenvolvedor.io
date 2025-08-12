using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models;

public class User : IdentityUser<Guid>
{
    public ICollection<TaskItem> TaskItems { get; set; }
    public string? GoogleId { get; set; }
}