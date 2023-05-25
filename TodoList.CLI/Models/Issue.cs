[Serializable]
public record class Issue : BaseModel
{
    public string Title { get; set; }

    public string Project { get; set; }

    public DateTime CreatedDate { get; private set; }

    public bool IsCompleted { get; set; }

    public Issue(string title, string project) : base()
    {
        Title = title;
        Project = project;
        CreatedDate = DateTime.UtcNow;
    }
}