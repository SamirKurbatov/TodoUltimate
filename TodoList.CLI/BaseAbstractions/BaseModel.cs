[Serializable]
public abstract record BaseModel
{
    public Guid UniqueId { get; }
    public string Title { get; set; }

    public BaseModel(string title)
    {
        Title = title;
        UniqueId = Guid.NewGuid();
    }
}