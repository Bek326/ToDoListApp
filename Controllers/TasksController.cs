using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ApplicationDbContext context, ILogger<TasksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, [FromBody] TaskItem task)
    {
        if (id != task.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            _context.Update(task);
            _context.SaveChanges();
            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update error");
            return StatusCode(500, "Internal server error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the task");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] bool? isCompleted, [FromQuery] int? priorityId)
    {
        var tasks = _context.Tasks.AsQueryable();

        if (isCompleted.HasValue)
        {
            tasks = tasks.Where(t => t.IsCompleted == isCompleted.Value);
        }

        if (priorityId.HasValue)
        {
            tasks = tasks.Where(t => t.PriorityId == priorityId.Value);
        }

        return Ok(await tasks.ToListAsync());
    }
}