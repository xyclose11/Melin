namespace Melin.Server.Models;

public class Task
{
    public required string Id { get; set; }
    public string? Title { get; set; }
    public string? Status { get; set; }
    public string? Label { get; set; }
    public string? Priority { get; set; }
}

public class TaskList
{
    public List<Task>? Tasks { get; set; }
}
