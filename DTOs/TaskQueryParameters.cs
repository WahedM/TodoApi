using System.ComponentModel.DataAnnotations;
using TodoApi.Models.Enums;

namespace TodoApi.DTOs
{
    public enum TaskSortBy { CreatedAt, Deadline, Priority, Title}
    public class TaskQueryParameters
    {
        public string? Search { get; set; }
        [EnumDataType(typeof(TaskItemStatus))]
        public TaskItemStatus? Status { get; set; }
        [EnumDataType(typeof(TaskItemPriority))]
        public TaskItemPriority? Priority { get; set; }
        public TaskSortBy SortBy { get; set; } = TaskSortBy.CreatedAt;
        public bool Descending { get; set; } = true;
        [Range(1, 100_000)]
        public int Page { get; set; } = 1;
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

    }
}
