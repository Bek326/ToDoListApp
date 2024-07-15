using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Priorities.Any())
            {
                return;   
            }

            context.Priorities.AddRange(
                new Priority
                {
                    Level = 1
                },
                new Priority
                {
                    Level = 2
                },
                new Priority
                {
                    Level = 3
                }
            );

            context.Users.AddRange(
                new User
                {
                    Name = "User1"
                },
                new User
                {
                    Name = "User2"
                }
            );

            context.SaveChanges();
        }
    }
}