[Serializable]
public record class Issue : BaseModel
{
    public string Title { get; set; }
    
    public DateTime CreatedDate { get; private set; }

    public bool IsCompleted { get; set; }    

    public Issue(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentNullException(nameof(title), "не может иметь пустое значение");
        }

        Title =  title;
        CreatedDate = DateTime.UtcNow;
    }
}