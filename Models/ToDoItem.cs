using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models;

public class ToDoItem
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    public string? Description { get; set; }
    public bool IsCompleted { get; set; }

    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    public int PriorityId { get; set; }
    public Priority? Priority { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}