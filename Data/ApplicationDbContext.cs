using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Priority>().HasKey(p => p.Id);
        modelBuilder.Entity<ToDoItem>().HasOne(t => t.Priority).WithMany(p => p.ToDoItems).HasForeignKey(t => t.PriorityId);
        modelBuilder.Entity<ToDoItem>().HasOne(t => t.User).WithMany(u => u.ToDoItems).HasForeignKey(t => t.UserId);
    }
}