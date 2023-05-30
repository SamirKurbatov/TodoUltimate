using TodoList.CLI.Models;
using TodoList.CLI.BaseAbstractions;
using TodoList.CLI.Interfaces;

namespace TodoList.CLI;

public class IssueGroupController : BaseConsoleController<IssueGroupModel>, IGroupController<IssueModel, IssueGroupModel>
{
    public event Action<IssueModel, IssueGroupModel>? IssueAdded;
    public IssueGroupController(IDataRepository<IssueGroupModel> repository, IFactoryModel<IssueGroupModel> creator) : base(repository, creator)
    {
    }

    public void OnIssueAdded(IssueModel issue, IssueGroupModel group)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Задача {issue.Title} была добавлена в группу {group.Title}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void AddModelToGroup(IssueModel model, IssueGroupModel group)
    {
        if (group.GroupModels.Contains(model))
        {
            Console.WriteLine($"Вы пытаетесь добавить существующую задачу");
            return;
        }
        group.GroupModels.Add(model);
        OnIssueAdded(model, group);
        Repository.SaveData(DataModels);
    }
}
