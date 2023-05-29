[Serializable]
public record BaseModel
{
    public Guid UniqueId { get; }
    public string Title { get; set; }

    public BaseModel()
    {
        UniqueId = Guid.NewGuid();
    }
}