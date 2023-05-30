using TodoList.CLI;
using TodoList.CLI.Models;

namespace TodoList.CLI.BaseAbstractions;
public class GroupModelFactory : BaseModelFactory<GroupModel>
{
    public override GroupModel Create()
    {
        Console.Write("Введите название группы: ");
        var title = CheckModelField("название группы");
        return new GroupModel(title);
    }
}