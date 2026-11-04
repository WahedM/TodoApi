using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs;
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

        public async Task<TaskItem> CreateTaskAsync(WriteTaskDto taskDto)
        {
            var task = mapper.Map<TaskItem>(taskDto);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> DeleteTaskAsync(Guid id)
        {
            var res = await context.Tasks.FindAsync(id);
            if (res is null)
                return null;
            context.Tasks.Remove(res);
            await context.SaveChangesAsync();
            return res;    
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            var res = await context.Tasks.ToListAsync();
            
            return res;
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
        {
            var res = await context.Tasks.FindAsync(id);
            if (res is null)
                return null;
            return res;
        }

        public async Task<TaskItem?> UpdateTaskAsync(WriteTaskDto taskDto, Guid id)
        {
            var res = await context.Tasks.FindAsync(id);
            if (res is null)
                return null;
            mapper.Map(taskDto, res);
            await context.SaveChangesAsync();
            return res;
        }
    }
}
