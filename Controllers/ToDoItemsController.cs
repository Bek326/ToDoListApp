using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ToDoItemsController(ApplicationDbContext context) 
    { 
        _context = context;
    }
    
    [HttpGet] 
    public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems() 
    { 
        return await _context.ToDoItems
            .Include(t => t.Priority)
            .Include(t => t.User)
            .ToListAsync();
    }
    
    [HttpGet("{id}")] 
    public async Task<ActionResult<ToDoItem>> GetToDoItem(int id) 
    { 
        var toDoItem = await _context.ToDoItems
            .Include(t => t.Priority)
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (toDoItem == null) 
        { 
            return NotFound();
        }
        
        return toDoItem;
    }
    
    [HttpPost] 
    public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem) 
    { 
        var existingPriority = await _context.Priorities.FindAsync(toDoItem.PriorityId); 
        var existingUser = await _context.Users.FindAsync(toDoItem.UserId);
        
        if (existingPriority == null || existingUser == null) 
        { 
            return BadRequest("Priority or User not found.");
        }
        
        toDoItem.Priority = existingPriority; 
        toDoItem.User = existingUser;
        
        _context.ToDoItems.Add(toDoItem); 
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
    }
    
    [HttpPut("{id}")] 
    public async Task<IActionResult> PutToDoItem(int id, ToDoItem toDoItem) 
    { 
        if (id != toDoItem.Id) 
        { 
            return BadRequest();
        }
        
        _context.Entry(toDoItem).State = EntityState.Modified;
        
        try 
        { 
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) 
        { 
            if (!ToDoItemExists(id)) 
            { 
                return NotFound();
            }
            else 
            { 
                throw;
            }
        }
        
        return NoContent();
    }
    
    [HttpDelete("{id}")] 
    public async Task<IActionResult> DeleteToDoItem(int id) 
    { 
        var toDoItem = await _context.ToDoItems.FindAsync(id); 
        if (toDoItem == null) 
        { 
            return NotFound();
        }
        
        _context.ToDoItems.Remove(toDoItem); 
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    private bool ToDoItemExists(int id) 
    { 
        return _context.ToDoItems.Any(e => e.Id == id);
    }
}