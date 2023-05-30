namespace TodoList.CLI.BaseAbstractions;

public class IssueModelFactory : BaseModelFactory
{
    public override BaseModel Create()
    {
        Console.Write("Введите название задачи: ");
        var title = CheckModelField("Название задачи: ");
        Console.Write("Введите описание задачи: ");
        var description = CheckModelField("Описание: ");
        return new IssueModel(title, description);
    }
}