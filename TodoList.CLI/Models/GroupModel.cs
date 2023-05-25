namespace TodoList.CLI.Models;
[Serializable]
public record GroupModel : BaseModel
{
    public DateTime CreatedDate { get; set; }
    public List<IssueModel> Issues { get; set; }

    public GroupModel(string title) : base(title)
    {
        CreatedDate = DateTime.UtcNow;
    }
}