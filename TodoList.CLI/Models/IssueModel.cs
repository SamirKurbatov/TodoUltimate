[Serializable]
public record class IssueModel : BaseModel
{
    public DateTime CreatedDate { get; private set; }

    public bool IsCompleted { get; set; }

    public string Description { get; private set; }

    public IssueModel(string title, string description) : base(title) 
    {
        CreatedDate = DateTime.UtcNow;
        Description = description;
    }
}