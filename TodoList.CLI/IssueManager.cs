namespace TodoList;
public class IssueManager : BaseManager<Issue>
{
    protected event Action<Issue, string> ChangeDataUpdate;
    public IssueManager(BaseRepository repository) : base(repository)
    {
        creator = new IssueCreator();
        ChangeDataUpdate += OnDataChangedUpdated;
    }

    public override void ChangeIsDone(int id)
    {
        ChangeData(id, (issue, isDoneInfo) =>
        {
            if (issue.IsCompleted == true)
            {
                Console.WriteLine($"Задача {id} уже отмечена как выполненная");
                return;
            }

            issue.IsCompleted = true;
            ChangeDataUpdate?.Invoke(issue, isDoneInfo);
        }, "отмечена как выполнен(ая)");

        repository.Save(models);
    }

    public override void Edit(int id, string title)
    {
        ChangeData(id, (issue, updateInfo) =>
        {
            issue.Title = title;
            ChangeDataUpdate?.Invoke(issue, updateInfo);
        }, "обновлен(а)");

        repository.Save(models);
    }

    protected override void OnAdded(Issue issue)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"Задача добавлена: {issue.Title}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected override void OnRemoved(int id)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"Задача удалена: {id}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected override void OnNotFound(int id)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"Задача не найдена: {id}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected override void OnDataChangedUpdated(Issue issue, string updateInfo)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"Задача {issue.UniqueId}: {updateInfo}: ");
        Console.ForegroundColor = ConsoleColor.White;
    }
}