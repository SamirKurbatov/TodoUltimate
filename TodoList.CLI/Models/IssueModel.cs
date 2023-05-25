[Serializable]
public record class IssueModel : BaseModel
{
    public DateTime CreatedDate { get; private set; }

    public bool IsCompleted { get; set; }

    public IssueModel(string title) : base(title)
    {
        Title = title;
        CreatedDate = DateTime.UtcNow;
    }
}