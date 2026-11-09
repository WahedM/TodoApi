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
        public async Task<PagedResult<TaskItem>> GetAllTasksAsync(TaskQueryParameters q)
        {
            var query = context.Tasks.AsNoTracking();

            // search + filters
            if (!string.IsNullOrWhiteSpace(q.Search))
            {
                var term = q.Search.Trim();
                query = query.Where(t => t.Title.Contains(term) || t.Description.Contains(term));
            }
            if (q.Status is not null) query = query.Where(t => t.Status == q.Status);
            if (q.Priority is not null) query = query.Where(t => t.Priority == q.Priority);

            // count AFTER filtering, BEFORE paging
            var totalCount = await query.CountAsync();

            // sorting
            IOrderedQueryable<TaskItem> ordered = q.SortBy switch
            {
                TaskSortBy.Title => q.Descending ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
                TaskSortBy.Deadline => q.Descending ? query.OrderByDescending(t => t.Deadline) : query.OrderBy(t => t.Deadline),
                TaskSortBy.Priority => q.Descending ? query.OrderByDescending(t => t.Priority) : query.OrderBy(t => t.Priority),
                _ => q.Descending ? query.OrderByDescending(t => t.CreatedAt) : query.OrderBy(t => t.CreatedAt)
            };
            ordered = ordered.ThenBy(t => t.Id);   // tie-breaker

            // paging
            var items = await ordered
                .Skip((q.Page - 1) * q.PageSize)
                .Take(q.PageSize)
                .ToListAsync();

            return new PagedResult<TaskItem>
            {
                Items = items,
                Page = q.Page,
                PageSize = q.PageSize,
                TotalCount = totalCount
            };
        }
        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
        {
            return await context.Tasks.FindAsync(id);
        }

        public async Task<TaskItem> CreateTaskAsync(WriteTaskDto taskDto)
        {
            var task = mapper.Map<TaskItem>(taskDto);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateTaskAsync(Guid id, WriteTaskDto taskDto)
        {
            var res = await context.Tasks.FindAsync(id);
            if (res is null)
                return null;
            mapper.Map(taskDto, res);
            await context.SaveChangesAsync();
            return res;
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

    }
}
