namespace to_do_list.Models;

public class UpdateToDoDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsDone { get; set; }
}