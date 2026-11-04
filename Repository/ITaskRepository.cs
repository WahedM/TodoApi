using TodoApi.DTOs;
using TodoApi.Models;
namespace TodoApi.Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(Guid id);
        Task<TaskItem> CreateTaskAsync(WriteTaskDto taskDto);
        Task<TaskItem?> UpdateTaskAsync(Guid id, WriteTaskDto taskDto);
        Task<TaskItem?> DeleteTaskAsync(Guid id);
    }
}
