using TodoApi.Models;
namespace TodoApi.Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task, int id);
        Task DeleteTaskAsync(int id);
    }
}
