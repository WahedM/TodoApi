
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
}
    public enum TaskStatus
    {
        Todo,
        inProgress,
        Done
    }