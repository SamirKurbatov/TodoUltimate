using System.Text;

namespace TodoList.CLI.Models;

[Serializable]
public record BaseGroupModel<T> : BaseModel
{
    public List<T> GroupModels { get; set; }
    public BaseGroupModel(string title) : base(title)
    {
        GroupModels = new();
    }

    public override string GetInfo()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Группа {Title} {typeof(T).Name}");

        foreach (var item in GroupModels)
        {
            if (item is BaseModel baseModel)
            {
                sb.AppendLine(baseModel.GetInfo());
            }
        }

        return sb.ToString();
    }
}