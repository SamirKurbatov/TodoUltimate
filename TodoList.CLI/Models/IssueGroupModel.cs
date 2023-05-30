using System.Text;
using TodoList.CLI.Repositories;

namespace TodoList.CLI.Models;
[Serializable]
public record IssueGroupModel : BaseGroupModel<IssueModel>
{
    public List<IssueModel> Issues { get; set; }

    public IssueGroupModel(string title) : base(title)
    {
        Issues = new();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Дата создания группы: {CreatedDate}");
        sb.AppendLine($"Название группы: {Title}");
        sb.AppendLine("Задачи:");

        foreach (var issue in GroupModels)
        {
            sb.AppendLine(issue.GetInfo());
        }

        return sb.ToString();
    }
}