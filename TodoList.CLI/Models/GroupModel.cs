using System.Text;
using TodoList.CLI.Repositories;

namespace TodoList.CLI.Models;
[Serializable]
public record GroupModel : BaseModel
{
    public DateTime CreatedDate { get; set; }
    public List<IssueModel> Issues { get; set; }

    public GroupModel(string title) : base(title)
    {
        CreatedDate = DateTime.UtcNow;
        Issues = new();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"Дата создания: {CreatedDate} Название группы: {Title}\nЗадачи:\n");

        foreach (var issue in Issues)
        {
            sb.Append($"Дата создания: {issue.CreatedDate}\nНазвание: {issue.Title}\nОтметка о выполнении:{issue.IsCompleted}");
        }
        return sb.ToString();
    }
}