namespace TodoList.CLI;

public class IssueConsoleCreator : BaseCreator<IssueModel>
{
    public override IssueModel Create()
    {
        Console.Write("Введите название задачи: ");
        var title = CheckField("задачи");
        return new IssueModel(title);
    }
}