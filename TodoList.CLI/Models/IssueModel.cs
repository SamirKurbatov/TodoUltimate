[Serializable]
public record class IssueModel : BaseModel
{
    public DateTime CreatedDate { get; private set; }

    public bool IsCompleted { get; set; }

    public IssueModel()
    {
        CreatedDate = DateTime.UtcNow;
    }
}