namespace TodoList;
public class IssueManager : BaseManager<Issue>
{
    public new event Action<Issue, string> ChangeDataUpdate;

    public IssueManager(IRepository repository, string path) : base(repository, path)
    {
        creator = new IssueCreator();
        ChangeDataUpdate += OnDataChangedUpdated;
    }

    public override void Add()
    {
        base.Add();
    }

    public override void Remove(int id)
    {
        base.Remove(id);
    }

    public override void ChangeIsDone(int id)
    {
        ChangeData(id, (issue, isDoneInfo) => {
            issue.IsCompleted = true;
            ChangeDataUpdate?.Invoke(issue, isDoneInfo);
        }, "отмечена как выполнен(ая)");
    }

    public override void Update(int id, string title)
    {
        ChangeData(id, (issue, updateInfo) => {
            issue.Title = title;
            ChangeDataUpdate?.Invoke(issue, updateInfo);
        }, "обновлен(а)");
    }

    public override void OnTaskAdded(Issue issue)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"Задача добавлена: {issue.Id}, {issue.Title}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public override void OnTaskRemoved(int id)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"Задача удалена: {id}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public override void OnTaskNotFound(int id)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"Задача не найдена: {id}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public override void OnDataChangedUpdated(Issue issue, string updateInfo)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"Задача {issue.Id} {updateInfo}: ");
        Console.ForegroundColor = ConsoleColor.White;
    }
}