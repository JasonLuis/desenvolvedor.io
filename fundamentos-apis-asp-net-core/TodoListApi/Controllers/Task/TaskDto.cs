using System.ComponentModel.DataAnnotations;
using TodoListApi.Models;

namespace TodoListApi.Controllers.Task;

public class CreateTaskDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    public string Title { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
    public string Description { get; set; }
}

public class UpdateTaskDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int IdTask { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    public string Title { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
    public string Description { get; set; }
}

public class UpdateStatusTaskDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int IdTask { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public StatusTask Status { get; set; }
}