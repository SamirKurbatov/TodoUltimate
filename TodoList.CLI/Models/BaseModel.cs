[Serializable]
public abstract record BaseModel
{
    public Guid UniqueId { get; }

    public BaseModel()
    {
        UniqueId = Guid.NewGuid();
    }
}