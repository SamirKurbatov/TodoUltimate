[Serializable]
public record class Issue : BaseModel
{
    public string Title { get; set; }

    public DateTime CreatedDate { get; private set; }

    public bool IsCompleted { get; set; }

    public Issue(string title) : base()
    {
        Title = title;
        CreatedDate = DateTime.UtcNow;
    }
}