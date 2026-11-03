
using System.ComponentModel.DataAnnotations;
using TodoApi.Models;
using TodoApi.Models.Enums;
namespace TodoApi.DTOs
{
    public class WriteTaskDto
    {
        [Required(ErrorMessage = "The title is required.")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? Deadline { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
        public TaskItemPriority Priority { get; set; } = TaskItemPriority.medium;
    }
}