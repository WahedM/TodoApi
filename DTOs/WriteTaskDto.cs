
using System.ComponentModel.DataAnnotations;
using TodoApi.Models;
using TodoApi.Models.Enums;
namespace TodoApi.DTOs
{
    public class WriteTaskDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        public DateTime? Deadline { get; set; }
        [EnumDataType(typeof(TaskItemStatus))]
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
        [EnumDataType(typeof(TaskItemPriority))]
        public TaskItemPriority Priority { get; set; } = TaskItemPriority.medium;
    }
}