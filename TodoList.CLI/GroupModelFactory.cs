using TodoList.CLI;
using TodoList.CLI.Models;

namespace TodoList.CLI.BaseAbstractions;
public class GroupModelFactory : BaseFactoryModel<IssueGroupModel>
{
    public override IssueGroupModel Create()
    {
        Console.Write("Введите название группы: ");
        var title = CheckModelField("название группы");
        return new IssueGroupModel(title);
    }
}