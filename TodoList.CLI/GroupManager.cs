using TodoList.CLI.Models;

namespace TodoList.CLI;

public class GroupManager : BaseManager<GroupModel>
{
    public event Action<IssueModel, GroupModel>? IssueAdded;
    public GroupManager(IDataRepository<GroupModel> repository, IModelCreator<GroupModel> creator) : base(repository, creator)
    {
    }

    public void AddIssueToGroup(IssueModel issue, GroupModel group)
    {
        group.Issues.Add(issue);
        OnIssueAdded(issue, group);
        if (group.Issues.Contains(issue))
        {
            Console.WriteLine("Вы пытаетесь добавить существующую задачу");
            return;
        }
        else
        {
            repository.Save(Models);
        }
    }

    public void OnIssueAdded(IssueModel issue, GroupModel group)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Задача {issue.Title} была добавлена в группу {group.Title}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}