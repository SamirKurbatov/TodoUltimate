using TodoList.CLI.BaseAbstractions;

namespace TodoList.CLI;
public class IssueController : BaseConsoleController<IssueModel>
{
    public IssueController(IDataRepository<IssueModel> repository, IFactoryModel<IssueModel> creator) : base(repository, creator)
    {
    }

    public void ChangeIsDone(int id)
    {
        ChangeData(id, data =>
        {
            data.IsCompleted = true;
            Repository.SaveData(DataModels);
        }, "отмечена как выполнена");
    }
}