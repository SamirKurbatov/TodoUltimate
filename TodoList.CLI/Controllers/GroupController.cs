using TodoList.CLI.Models;
using TodoList.CLI.BaseAbstractions;
namespace TodoList.CLI;

public class GroupController : BaseConsoleController<GroupModel>
{
    public event Action<IssueModel, GroupModel>? IssueAdded;
    public GroupController(IDataRepository<GroupModel> repository, IModelFactory creator) : base(repository, creator)
    {
    }

    public void AddIssueToGroup(IssueModel issue, GroupModel group)
    {
        if (group.Issues.Contains(issue))
        {
            Console.WriteLine("Вы пытаетесь добавить существующую задачу");
            return;
        }
        group.Issues.Add(issue);
        OnIssueAdded(issue, group);
        repository.SaveData(DataModels);
    }

    public void OnIssueAdded(IssueModel issue, GroupModel group)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Задача {issue.Title} была добавлена в группу {group.Title}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}