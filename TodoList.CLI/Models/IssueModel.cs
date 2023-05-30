[Serializable]
public record class IssueModel : BaseModel
{
    public bool IsCompleted { get; set; }

    public string Description { get; private set; }

    public IssueModel(string title, string description) : base(title)
    {
        Description = description;
    }

    public override string GetInfo()
    {
        return $"\nДата создания: {CreatedDate}\nНазвание: {Title}\nОписание: {Description}";
    }
}