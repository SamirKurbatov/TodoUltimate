using System.Text;

namespace TodoList.CLI.Models;
[Serializable]
public record GroupModel : BaseModel
{
    public DateTime CreatedDate { get; set; }
    public List<IssueModel> Issues { get; set; }

    public GroupModel(string title) : base(title)
    {
        CreatedDate = DateTime.UtcNow;
        Issues = new List<IssueModel>();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Название группы: {Title}, Дата создания: {CreatedDate}, Задачи: ");

        foreach (var issue in Issues)
        {
            sb.AppendLine($"Название: {issue.Title} Дата создания: {issue.CreatedDate} Отметка о выполнении:{issue.IsCompleted}");
        }
        return sb.ToString();
    }
}