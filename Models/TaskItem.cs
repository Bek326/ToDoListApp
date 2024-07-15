using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models;

public class TaskItem
{
    public int Id { get; set; }
        
    [Required]
    public string? Title { get; set; }
        
    public string? Description { get; set; }
        
    public bool IsCompleted { get; set; }
        
    public DateTime? DueDate { get; set; }
        
    public int PriorityId { get; set; }
        
    public int UserId { get; set; }
}