[Serializable]
public abstract record BaseModel
{
    public int Id { get; set; }

    public Guid UniqueId { get; init; }

    public BaseModel()
    {
        UniqueId = Guid.NewGuid();
    }
}