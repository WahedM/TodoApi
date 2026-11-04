using TodoApi.Models.Enums;

namespace TodoApi.DTOs
{
    public class TaskResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskItemStatus Status { get; set; }
        public TaskItemPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
