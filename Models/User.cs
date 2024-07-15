using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public string? Name { get; set; }
    public List<ToDoItem>? ToDoItems { get; set; }
}