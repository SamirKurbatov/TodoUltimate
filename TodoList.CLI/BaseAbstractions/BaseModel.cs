[Serializable]
public abstract record BaseModel
{
    public string Title { get; set; }
    public DateTime CreatedDate { get; private set; }
    public Guid UniqueId { get; }

    public BaseModel(string title)
    {
        Title = title;
        CreatedDate = DateTime.UtcNow;
        UniqueId = Guid.NewGuid();
    }

    public abstract string GetInfo();
}