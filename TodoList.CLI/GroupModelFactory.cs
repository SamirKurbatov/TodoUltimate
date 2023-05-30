using TodoList.CLI;
using TodoList.CLI.Models;

namespace TodoList.CLI.BaseAbstractions;
public class GroupModelFactory : BaseModelFactory
{
    public override BaseModel Create()
    {
        Console.Write("Введите название группы: ");
        var title = CheckModelField("название группы");
        return new GroupModel(title);
    }
}