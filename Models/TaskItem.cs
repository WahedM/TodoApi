namespace TodoApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public TaskPriority priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
    }
    public enum TaskStatus
    {
        Todo,
        inProgress,
        Done
    }
    public enum TaskPriority
    {
        low,
        medium,
        high
    }
}