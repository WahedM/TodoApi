using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public TaskRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(int id)
        {
            var res = await context.Tasks.FindAsync(id);
            if (res is null)
                throw new Exception("No such a Task with the given ID");
            context.Tasks.Remove(res);
            await context.SaveChangesAsync();   
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            var res = await context.Tasks.ToListAsync();

            return res;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            var res = await context.Tasks.FindAsync(id);
            if (res is null)
                throw new Exception("No such a Task with the given ID");

            return res;
        }

        public async Task UpdateTaskAsync(TaskItem task, int id)
        {
            var res = await context.Tasks.FindAsync(id);
            if (res is null)
                throw new Exception("No such a Task with the given ID");
            res = task;
            await context.SaveChangesAsync();
        }
    }
}
