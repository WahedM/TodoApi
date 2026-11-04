using TodoApi.DTOs;
using TodoApi.Models;
namespace TodoApi.Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(Guid id);
        Task<TaskItem> CreateTaskAsync(WriteTaskDto taskDto);
        Task<TaskItem?> UpdateTaskAsync(WriteTaskDto taskDto, Guid id);
        Task<TaskItem?> DeleteTaskAsync(Guid id);
    }
}
