
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}
