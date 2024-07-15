using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoListApp.Models;

public class Priority
{
    [Key]
    public int Id { get; set; }
    public int Level { get; set; }
    
    [JsonIgnore]
    public List<ToDoItem>? ToDoItems { get; set; }
}