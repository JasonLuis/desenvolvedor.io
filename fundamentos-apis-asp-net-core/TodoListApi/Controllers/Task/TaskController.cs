using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using TodoListApi.Data;
using TodoListApi.Models;

namespace TodoListApi.Controllers.Task;

[Authorize]
[ApiController]
[Route("api/tasks")]
public class TaskController: ControllerBase
{

    private readonly TodoListDbContext _context;

    public TaskController(TodoListDbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTasks()
    {
        if (_context.Tasks == null) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null) return Problem("Erro ao Listar as tasks, por favor contate o suporte.");

        var userIdGuid = Guid.Parse(userId);

        var tasks = await _context.Tasks.Where(t => t.UserId == userIdGuid)
                                        .Select(t => new 
                                        { Id = t.Id,
                                          Title = t.Title,
                                          Description = t.Description,
                                          Status = GetNameEnum(t.Status),
                                          CreatedAt = t.DtCreated.ToString("yyyy-MM-dd")
                                        })
                                        .ToListAsync();

        if (tasks is null) return NotFound();

        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        if (_context.Tasks == null) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null) return Problem("Erro ao Listar a task, por favor contate o suporte.");

        var userIdGuid = Guid.Parse(userId);

        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userIdGuid);

        if (task is null) return NotFound();

        return Ok(task);
    }

    [HttpGet("{status}")]
    public async Task<IActionResult> GetTaskByStatus(StatusTask status)
    {
        if (_context.Tasks == null) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null) return Problem("Erro ao Listar a task, por favor contate o suporte.");

        var userIdGuid = Guid.Parse(userId);

        var tasks = await _context.Tasks.Where(t => t.UserId == userIdGuid && t.Status == status)
                                        .ToListAsync();

        if (tasks is null) return NotFound();

        return Ok(tasks);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTask(CreateTaskDto task)
    {
        if (_context.Tasks == null) return Problem("Erro ao criar uma task, por favor contate o suporte.");

        if (!ModelState.IsValid) // verifica se o modelo é válido
            return ValidationProblem(ModelState); // retorna os erros de validação

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null) return Problem("Erro ao criar uma task, por favor contate o suporte.");

        var Task = new TaskItem
        {
            Title = task.Title,
            Description = task.Description,
            UserId = Guid.Parse(userId)
        };

        _context.Tasks.Add(Task);
        await _context.SaveChangesAsync();

        return Ok(task); // retorna a tarefa criada
    }

    [HttpPut("edit")]
    public async Task<IActionResult> UpdateTask(UpdateTaskDto dto)
    {
        if (_context.Tasks == null) return Problem("Erro ao criar uma task, por favor contate o suporte.");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Problem("Erro ao criar uma task, por favor contate o suporte.");

        if (!ModelState.IsValid) // verifica se o modelo é válido
            return ValidationProblem(ModelState); // retorna os erros de validação

        var userIdGuid = Guid.Parse(userId);

        var task = await _context.Tasks.Where(t => t.UserId == userIdGuid && t.Id == dto.IdTask)
                                        .FirstOrDefaultAsync();

        if (task is null)
        {
            return NotFound();
        }

        task.Title = dto.Title;
        task.Description = dto.Description;

        await _context.SaveChangesAsync();
        return Ok(task);
    }

    [HttpPatch("update-status")]
    public async Task<IActionResult> UpdateStatus(UpdateStatusTaskDto dto)
    {
        if (_context.Tasks == null) return Problem("Erro ao criar uma task, por favor contate o suporte.");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Problem("Erro ao criar uma task, por favor contate o suporte.");

        if (!ModelState.IsValid) // verifica se o modelo é válido
            return ValidationProblem(ModelState); // retorna os erros de validação

        var userIdGuid = Guid.Parse(userId);

        var task = await _context.Tasks.Where(t => t.UserId == userIdGuid && t.Id == task.IdTask)
                                        .FirstOrDefaultAsync();

        if(task is null)
        {
            return NotFound();
        }

        task.Status = task.Status;

        await _context.SaveChangesAsync();
        return Ok(task);
    }



    private static string GetNameEnum(StatusTask status)
    {
        return Enum.GetName(status) ?? string.Empty;
    }
}
